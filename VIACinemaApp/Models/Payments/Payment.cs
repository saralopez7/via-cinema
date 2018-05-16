using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace VIACinemaApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string CredictCard { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public int SecurityCode { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
    }
}