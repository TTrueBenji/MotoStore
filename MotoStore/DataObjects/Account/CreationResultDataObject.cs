using System.Collections.Generic;
using CommonData;

namespace MotoStore.DataObjects.Account
{
    public class CreationResultDataObject
    {
        public List<string> Errors { get; set; }
        public ResultOfCreation Result { get; set; }
    }
}