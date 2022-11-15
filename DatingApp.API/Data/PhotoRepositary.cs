using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.DTOS;
using DatingApp.API.Entities;
using DatingApp.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class PhotoRepositary : IPhotoRepository
    {
        public DataContext _context { get; }
        public IMapper _mapper { get; }

        public PhotoRepositary(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Photo> GetPhotoById(int Id)
        {
            return await _context.Photos
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<PhotoForApprovalDto>> GetUnApprovedPhotos()
        {
            return await _context.Photos
                .IgnoreQueryFilters()
                .Where(a => a.IsApproved == false)
                .Select(u => new PhotoForApprovalDto
                        {
                        Id = u.Id,
                        OwnerUserName = u.AppUser.UserName,
                        Url = u.Url,
                        IsApproved = u.IsApproved
                        })
                .ToListAsync();
        }

        public void RemovePhoto(Photo photo)
        {
             _context.Photos.Remove(photo);
        }


    }
}