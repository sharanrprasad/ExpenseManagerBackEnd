
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagerBackEnd.Models.ApiModels {
    
    [NotMapped]
    public class ErrorModel<T> {
        
        public int ErrorCode{ get; set; }
        public T Payload{ get; set; }

        public ErrorModel(int errorCode) {
            this.ErrorCode = errorCode;
        }

        public ErrorModel(int errorCode, T payload) {
            this.ErrorCode = errorCode;
            this.Payload = payload;
        }
    }

}