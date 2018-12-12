using JotFinalProject.Data;
using JotFinalProject.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models.Services
{
    public class NoteService : INote
    {
        private JotDbContext _context;

        public NoteService(JotDbContext context)
        {
            _context = context;
        }
        public Note GetNote(int id)
        {
            return _context.Notes.FirstOrDefault(x => x.ID == id);
        }
        public async Task UpdateNote(Note note)
        {
             _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
