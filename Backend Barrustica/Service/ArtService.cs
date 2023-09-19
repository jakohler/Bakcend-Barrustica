using Backend_Barrustica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Backend_Barrustica.Service
{
    public interface IArtService
    {
        Taller GetTaller(int tallerId);
        void AddTaller(string name, string description, string image);
        Task<List<Taller>> GetListTaller();
        void AddSeminario(string name, string description, string image);
        Task<List<Seminario>> GetListSeminario();
        Piece GetPiece(int pieceId);
        void AddPiece(string name, string description, string style, string image, int idArtist);
        Task<List<Piece>> GetListPiece();
        void DeleteItem(string image, string type);
    }
    public class ArtService : IArtService
    {
        public Taller GetTaller(int tallerId)
        {
            Taller result;
            using (var context = new BarrusticaDbContext())
            {
                result = context.TallerEntity.First(a => a.Id == tallerId);
            }

            return result;
        }
        public void AddTaller(string name, string description, string image)
        {            
            using (var context = new BarrusticaDbContext())
            {
                var newTaller = new Taller
                {
                    Name = name,
                    Description = description,
                    Image = image
                };

                context.TallerEntity.Add(newTaller);
                context.SaveChanges();
            }
        }
        public async Task<List<Taller>> GetListTaller()
        {
            List<Taller> result;
            using (var context = new BarrusticaDbContext())
            {
                result = await context.TallerEntity.ToListAsync();
            }

            return result;
        }
        public void AddSeminario(string name, string description, string image)
        {
            using (var context = new BarrusticaDbContext())
            {
                var newSeminario = new Seminario
                {
                    Name = name,
                    Description = description,
                    Image = image
                };

                context.SeminarioEntity.Add(newSeminario);
                context.SaveChanges();
            }
        }
        public async Task<List<Seminario>> GetListSeminario()
        {
            List<Seminario> result;
            using (var context = new BarrusticaDbContext())
            {
                result = await context.SeminarioEntity.ToListAsync();
            }

            return result;
        }
        public Piece GetPiece(int pieceId)
        {
            Piece result;
            using (var context = new BarrusticaDbContext())
            {
                result = context.PieceEntity.First(a => a.Id == pieceId);
            }

            return result;
        }
        public void AddPiece(string name, string description, string style, string image, int idArtist)
        {
            using (var context = new BarrusticaDbContext())
            {
                var newPiece = new Piece
                {
                    Name = name,
                    Description = description,
                    Style = style,
                    Image = image,
                    IdArtist = idArtist
                };

                context.PieceEntity.Add(newPiece);
                context.SaveChanges();
            }
        }
        public async Task<List<Piece>> GetListPiece()
        {
            List<Piece> result;
            using (var context = new BarrusticaDbContext())
            {
                result = await context.PieceEntity.ToListAsync();
            }

            return result;
        }
        public async void DeleteItem(string image, string type)
        {
            using (var context = new BarrusticaDbContext())
            {
                if(type == "taller")
                {
                    Taller? item;
                    item = await context.TallerEntity.FirstOrDefaultAsync(a => a.Image == image);
                    context.TallerEntity.Remove(item);
                }
                else if (type == "piece")
                {
                    Piece? item;
                    item = await context.PieceEntity.FirstOrDefaultAsync(a => a.Image == image);
                    context.PieceEntity.Remove(item);
                }
                else if (type == "seminario")
                {
                    Seminario? item;
                    item = await context.SeminarioEntity.FirstOrDefaultAsync(a => a.Image == image);
                    context.SeminarioEntity.Remove(item);
                }
                else
                {
                    throw new Exception("La entidad no se encontró o ya fue eliminada.");
                }

                context.SaveChanges();
            }
        }
    }
}
