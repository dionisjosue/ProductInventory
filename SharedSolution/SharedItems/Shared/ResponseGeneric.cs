using System;
using SharedItems.Shared;

namespace SharedLibrary.SharedItems.Shared
{
    public class ResponseGeneric<T> where T : class
    {
        public ResponseGeneric()
        {
            Result = new StatusResult();
            Result.Message = "success";
        }
        public T Data { get; set; }

        public IEnumerable<T> Items { get; set; }

        public StatusResult Result { get; set; }
    }

    public class ResponseGenericStruct<T> where T : struct
    {
        public ResponseGenericStruct()
        {
            Result = new StatusResult();
            Result.Message = "success";
        }
        public T Data { get; set; }

        public IEnumerable<T> Items { get; set; }

        public StatusResult Result { get; set; }

    }
}

