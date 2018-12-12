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

        public async void UpdateNote(Note note)
        {
            var n = _context.Notes.FirstOrDefault(x => x.ID == note.ID);
            n = note;
            await _context.SaveChangesAsync();
        }
    }
}
