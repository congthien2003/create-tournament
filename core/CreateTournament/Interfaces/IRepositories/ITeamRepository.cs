﻿using CreateTournament.Models;

namespace CreateTournament.Interfaces.IRepositories
{
    public interface ITeamRepository<TTeam> where TTeam : Team
    {
        Task<TTeam> CreateAsync(Team team);
        Task<TTeam> UpdateAsync(int id, string name, bool includeDeleted = false);
        Task<TTeam> UpdateImage(int id, string path, bool includeDeleted = false);

        Task<List<TTeam>> GetAllByIdTournamentAsync(int IdTournament, bool includeDeleted = false);
        Task<TTeam> GetTeamByIdAsync (int Id, bool includeDeleted = false);
        Task<TTeam> FindByIdAsync(int Id, bool includeDeleted = false);
        Task<List<TTeam>> CreateListTeamAsync(int quantity, int idTournament);
        Task<List<TTeam>> GetListTeamSwap(int idTournament, int round);

        
    }
}
