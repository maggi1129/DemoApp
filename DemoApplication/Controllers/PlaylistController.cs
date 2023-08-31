using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoApplication.Models;
using MongoExample.Models;

namespace DemoApplication.Controllers;
[Controller]
[Route("playlist")]
public class PlaylistController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public PlaylistController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Playlist>> Get()
    {
        var playlist = await _mongoDBService.GetAsync();
        return playlist;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Playlist playlist)
    {
        if (playlist == null)
        {
            return BadRequest("No playlist inserted");
        }

        await _mongoDBService.CreateAsync(playlist);
        //return Ok(playlist);
        return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId)
    {
        await _mongoDBService.AddToPlaylistAsync(id, movieId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return Ok();
    }


}
