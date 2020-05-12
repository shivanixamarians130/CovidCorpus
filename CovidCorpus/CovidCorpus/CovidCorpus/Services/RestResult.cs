namespace CovidCorpus.Services
{
    class RestResult : IRestResult
    {
        public bool IsSuccess
        {
            get
            {
                return StatusCode == 200;
            }
        }
        public string Message { get; set; }
        /// <summary>
        /// OK = 200, BadRequest = 400, InternalServerError = 500
        /// </summary>
        public int StatusCode { get; set; }
    }

    class RestResult<T> : RestResult, IRestResult<T>
    {
        public T Data { get; set; }
    }
}
