using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarefaAPI.Models
{   [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusTarefa
    {
        [Description("Finalizado")]
        Finalizado,
        [Description("Pendente")]
        Pendente
    }
}