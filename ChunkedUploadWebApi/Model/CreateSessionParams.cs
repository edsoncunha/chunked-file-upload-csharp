using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System.ComponentModel.DataAnnotations;
using System;

namespace ChunkedUploadWebApi.Model
{
    /// <summary>
    /// Session creation params
    /// </summary>
    [Serializable]
    public class CreateSessionParams
    {
        /// <summary>
        /// Size of data block (in bytes)
        /// </summary>
        [Required]
        public int? ChunkSize { get; set; }

        /// <summary>
        /// Total file size (in bytes)
        /// </summary>
        [Required]
        public long? TotalSize { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        [Required]
        public string FileName { get; set; }
    }
}