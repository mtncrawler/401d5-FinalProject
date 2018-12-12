using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models.Interfaces
{
    public interface INote
    {
        Task UpdateNote(Note note);

        Note GetNote(int id);
    }
}
