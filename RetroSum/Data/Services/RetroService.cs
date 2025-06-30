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

    public async Task<RetroItem> GetById(int id)
    {
        RetroItem item = await _db.Retros.FindAsync(id);
        return item;
    }

    public async Task<RetroItem> Save(RetroItem model)
    {
        _db.Retros.Add(model);
        await _db.SaveChangesAsync();
        return model;
    }

    public async Task Update(RetroItem? model)
    {
        var retro = await _db.Retros.FindAsync(model.Id);
        if (retro == null)
            return;
        
        retro.Title = model.Title;
        retro.Description = model.Description;
        retro.Content = model.Content;
        await _db.SaveChangesAsync();
    }

    public async Task<RetroItem> Remove(int id)
    {
        var retro = await _db.Retros.FindAsync(id);
        if (retro != null)
        {
            _db.Retros.Remove(retro);
            await _db.SaveChangesAsync();
            return retro;
        }

        return null;
    }
}