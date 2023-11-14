using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

internal class ProblemRepository : IProblemRepository
{
    private readonly DbSet<Problem> _problems;
    private readonly StakeholdersContext _context;

    public ProblemRepository(StakeholdersContext context)
    {
        _context = context;
        _problems = _context.Set<Problem>();
    }

    public PagedResult<Problem> GetByTouristId(int userId, int page, int pageSize)
    {
        var problems = _problems.Include(p => p.Messages).Where(p => p.IdTourist == userId).GetPagedById(page, pageSize);
        return problems.Result;
    }

    public PagedResult<Problem> GetByGuideId(int guideId, int page, int pageSize)
    {
        var problems = _problems.Include(p => p.Messages).Where(p => p.IdGuide == guideId).GetPagedById(page, pageSize);
        return problems.Result;
    }

    public PagedResult<Problem> GetUnsolvedProblems(int page, int pageSize)
    {
        var problems = _problems.Include(p => p.Messages).Where(p => p.IsSolved == false).GetPagedById(page, pageSize);
        return problems.Result;
    }

    public PagedResult<Problem> GetProblemsOfUser(int userId, int page, int pageSize)
    {
        var problems = _problems.Include(p => p.Messages).Where(p =>p.IdGuide == userId || p.IdTourist == userId).GetPagedById(page, pageSize);
        return problems.Result;
    }
}

