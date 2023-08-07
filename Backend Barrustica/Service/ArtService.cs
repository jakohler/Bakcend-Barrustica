using Backend_Barrustica.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Backend_Barrustica.Service
{
    public interface IArtService
    {
        Artist GetArtist(int artistId);
        void AddArtist(string name, string description, string image);
        Piece GetPiece(int pieceId);
        void AddPiece(string name, string description, string style, string image);
        Task<List<Piece>> GetListPiece();
    }
    public class ArtService : IArtService
    {
        public Artist GetArtist(int artistId)
        {
            Artist result;
            using (var context = new BarrusticaDbContext())
            {
                result = context.ArtistEntity.FirstOrDefault(a => a.Id == artistId);
            }

            return result;
        }
        public void AddArtist(string name, string description, string image)
        {            
            using (var context = new BarrusticaDbContext())
            {
                var newArtist = new Artist
                {
                    Name = name,
                    Description = description,
                    Image = image
                };

                context.ArtistEntity.Add(newArtist);
                context.SaveChanges();
            }
        }
        public Piece GetPiece(int pieceId)
        {
            Piece result;
            using (var context = new BarrusticaDbContext())
            {
                result = context.PieceEntity.FirstOrDefault(a => a.Id == pieceId);
            }

            return result;
        }
        public void AddPiece(string name, string description, string style, string image)
        {
            using (var context = new BarrusticaDbContext())
            {
                var newPiece = new Piece
                {
                    Name = name,
                    Description = description,
                    Style = style,
                    Image = image
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
    }
}
