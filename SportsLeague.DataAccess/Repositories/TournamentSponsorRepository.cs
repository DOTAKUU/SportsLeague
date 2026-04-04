using SportsLeague.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.DataAccess.Repositories
{
    public class TournamentSponsorRepository : ITournamentSponsorRepository
    {
        private readonly LeagueDbContext _context;

        public TournamentSponsorRepository(LeagueDbContext context)
        {
            _context = context;
        }

        public async Task<TournamentSponsor> CreateAsync(TournamentSponsor entity)
        {
            _context.TournamentSponsors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId)
        {
            return await _context.TournamentSponsors
                .Include(ts => ts.Tournament)
                .Where(ts => ts.SponsorId == sponsorId)
                .ToListAsync();
        }

        public async Task<TournamentSponsor> GetAsync(int sponsorId, int tournamentId)
        {
            return await _context.TournamentSponsors
                .FirstOrDefaultAsync(ts => ts.SponsorId == sponsorId && ts.TournamentId == tournamentId);
        }

        public async Task DeleteAsync(TournamentSponsor entity)
        {
            _context.TournamentSponsors.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
