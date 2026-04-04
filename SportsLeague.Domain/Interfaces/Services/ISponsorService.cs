using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<SponsorResponseDTO>> GetAllAsync();
        Task<SponsorResponseDTO> GetByIdAsync(int id);
        Task<SponsorResponseDTO> CreateAsync(SponsorRequestDTO dto);
        Task UpdateAsync(int id, SponsorRequestDTO dto);
        Task DeleteAsync(int id);

        Task LinkTournamentAsync(int sponsorId, TournamentSponsorRequestDTO dto);
        Task<List<TournamentSponsorResponseDTO>> GetTournaments(int sponsorId);
        Task UnlinkTournament(int sponsorId, int tournamentId);
    }
}
