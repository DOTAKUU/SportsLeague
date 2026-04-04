using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentSponsorRepository
    {
        Task<TournamentSponsor> CreateAsync(TournamentSponsor entity);
        Task<List<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId);
        Task<TournamentSponsor> GetAsync(int sponsorId, int tournamentId);
        Task DeleteAsync(TournamentSponsor entity);
    }
}
