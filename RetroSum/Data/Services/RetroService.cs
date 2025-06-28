using Microsoft.EntityFrameworkCore;
using RetroSum.Data.Context;
using RetroSum.Data.Models;

namespace RetroSum.Data.Services;

public class RetroService
{
    private readonly RetroSumContext _db;

    public RetroService(RetroSumContext db)
    {
        _db = db;
    }

    public async Task<List<RetroItem>> GetAll()
    {
        var query = _db.Retros.AsQueryable();
        return await query.ToListAsync();
    }
}