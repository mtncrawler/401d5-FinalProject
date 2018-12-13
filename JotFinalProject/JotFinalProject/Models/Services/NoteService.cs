using JotFinalProject.Data;
using JotFinalProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Note> GetNote(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(x => x.ID == id);
        }
        public async Task UpdateNote(Note note)
        {
             _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
