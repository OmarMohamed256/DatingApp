using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DTOS;
using DatingApp.API.Entities;

namespace DatingApp.API.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<PhotoForApprovalDto>> GetUnApprovedPhotos();
        Task<Photo> GetPhotoById(int Id);
        void RemovePhoto(Photo photo);
    }
}