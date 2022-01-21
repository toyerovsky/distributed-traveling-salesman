using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DistributedTravelingSalesman.Domain.Entities;
using DistributedTravelingSalesman.Dto;
using RestSharp;

namespace DistributedTravelingSalesman
{
    public class WorkerConnectionHelper
    {
        private readonly Graph _graph;
        private readonly RestClient _restClient;
        private readonly Worker _worker;

        private WorkerConnectionHelper(Worker worker, Graph graph)
        {
            _restClient = new RestClient(worker.Url);
            _worker = worker;
            _graph = graph;
        }

        public static Task<WorkerConnectionHelper> Create(Worker worker,
            Graph graph)
        {
            var task = Task.Run(async () =>
            {
                var helper = new WorkerConnectionHelper(worker, graph);
                await helper.Initialize();

                return helper;
            });

            return task;
        }

        public async Task Initialize()
        {
            var registerRequest = new RestRequest("/api/task/begin");

            registerRequest.AddJsonBody(new BeginTaskRequestDto
            {
                Graph = _graph
            });
            var response = await _restClient.ExecutePostAsync(registerRequest).ConfigureAwait(false);
            if (!response.IsSuccessful) ThrowExceptionOnNotSuccessfulResponse(response);
        }

        public async Task<FindBestPartialResultResponseDto> CalculateFor(int begin, IList<int> chunks)
        {
            var request = new RestRequest("/api/task/findBestPartialResult");
            request.Timeout = Timeout.Infinite;
            request.AddJsonBody(new FindBestPartialResultRequestDto
            {
                StartIndex = begin,
                Chunks = chunks
            });
            var response = await _restClient.ExecutePostAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessful) ThrowExceptionOnNotSuccessfulResponse(response);

            return JsonSerializer.Deserialize<FindBestPartialResultResponseDto>(response.Content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task FinalizeSession()
        {
            var request = new RestRequest("/api/task");
            var response = await _restClient.DeleteAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessful) ThrowExceptionOnNotSuccessfulResponse(response);
        }

        private void ThrowExceptionOnNotSuccessfulResponse(RestResponseBase response)
        {
            throw response.ResponseStatus == ResponseStatus.Completed
                ? new InvalidOperationException(response.ErrorMessage)
                : new InvalidOperationException($"Worker not responding ({_worker.Url})");
        }
    }
}