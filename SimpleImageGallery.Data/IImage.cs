﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using SimpleImageGallery.Data.Models;

namespace SimpleImageGallery.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        GalleryImage GetById(int id);
        CloudBlobContainer GetBlobContainer(string connectionString, string containerName);
        Task SetImage(string title, string tags, Uri uri);
        List<ImageTag> ParseTags(string tags);
    }
}
