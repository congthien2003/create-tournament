﻿using AutoMapper;
using CreateTournament.Data;
using CreateTournament.Interfaces.IRepositories;
using CreateTournament.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateTournament.Repositories
{
    public class TournamentRepository : ITournamentRepository<Tournament>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        
        public TournamentRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<Tournament> Create(Tournament tournament)
        {
            var newTournament = mapper.Map<Tournament>(tournament);
            _dataContext.Tournaments.Add(newTournament);
            await _dataContext.SaveChangesAsync();
            return newTournament; 
        }

        public async Task<Tournament> Delete(int id)
        {
            var tournament = await _dataContext.Tournaments
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            tournament.IsDeleted = true;
            await _dataContext.SaveChangesAsync();
            return tournament;
        }

        public async Task<List<Tournament>> GetAll(bool incluDeleted = false)
        {
            return await _dataContext.Tournaments
                .Where(x=>x.IsDeleted == incluDeleted)
                .ToListAsync();
        }

        public async Task<Tournament> GetByIdTournament(int id, bool incluDeleted = false)
        {
            return await _dataContext.Tournaments
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
        }

        public async Task<List<Tournament>> GetTourByUserId(int userId, bool incluDeleted = false)
        {
            return await _dataContext.Tournaments
                .Where(x => x.UserId == userId && x.IsDeleted==incluDeleted)
                .ToListAsync();
        }

        public async Task<Tournament> Update(Tournament tournament, bool incluDeleted = false)
        {
            var exists = await _dataContext.Tournaments
                .FirstOrDefaultAsync(x=>x.Id == tournament.Id && x.IsDeleted == incluDeleted);
            if (exists == null)
            {
                return null;
            }
            else
            {
                exists.Name = tournament.Name;
                exists.Created = DateTime.Now;
                exists.QuantityTeam = tournament.QuantityTeam;
                exists.Location = tournament.Location;
                exists.FormatTypeId = tournament.FormatTypeId;
                exists.SportTypeId = tournament.SportTypeId;
            }
            await _dataContext.SaveChangesAsync();
            return exists;
        }

        public async Task<List<Tournament>> SearchTournaments(string searchTerm, bool incluDeleted = false)
        {
            var tournament = _dataContext.Tournaments
                .Where(t => t.Name.Contains(searchTerm));
            if (!incluDeleted)
            {
                tournament = tournament.Where(x=>x.IsDeleted == incluDeleted);
            }
            var searchTour = await tournament.ToListAsync();
            return searchTour;
        }
    }
}