using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _repo;
        private readonly ITournamentSponsorRepository _tsRepo;
        private readonly IMapper _mapper;
        private readonly LeagueDbContext _context;

        public SponsorService(ISponsorRepository repo, ITournamentSponsorRepository tsRepo, IMapper mapper, LeagueDbContext context)
        {
            _repo = repo;
            _tsRepo = tsRepo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<SponsorResponseDTO> CreateAsync(SponsorRequestDTO dto)
        {
            if (await _repo.ExistsByNameAsync(dto.Name))
                throw new InvalidOperationException("Duplicate name");

            if (!new EmailAddressAttribute().IsValid(dto.ContactEmail))
                throw new InvalidOperationException("Invalid email");

            var sponsor = _mapper.Map<Sponsor>(dto);
            sponsor.CreatedAt = DateTime.UtcNow;

            await _repo.CreateAsync(sponsor);

            return _mapper.Map<SponsorResponseDTO>(sponsor);
        }

        public async Task LinkTournamentAsync(int sponsorId, TournamentSponsorRequestDTO dto)
        {
            var sponsor = await _repo.GetByIdAsync(sponsorId);
            var tournament = await _context.Tournaments.FindAsync(dto.TournamentId);

            if (sponsor == null || tournament == null)
                throw new KeyNotFoundException();

            var existing = await _tsRepo.GetAsync(sponsorId, dto.TournamentId);
            if (existing != null)
                throw new InvalidOperationException("Duplicate relation");

            if (dto.ContractAmount <= 0)
                throw new InvalidOperationException("Invalid contract");

            await _tsRepo.CreateAsync(new TournamentSponsor
            {
                SponsorId = sponsorId,
                TournamentId = dto.TournamentId,
                ContractAmount = dto.ContractAmount,
                JoinedAt = DateTime.UtcNow
            });
        }

        // otros métodos CRUD normales...
    }
}
