﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Servers
    {

        private readonly Context _context;

        public Servers(Context context)
        {
            _context = context;
        }
        
        public async Task NumberOfCommitments(ulong UserId, int CommitmentNumber)
        {
           
            var server = await _context.Servers
                .FindAsync(UserId);
            
            if (server == null)
                _context.Add(new Server {Id = UserId,  UserId = UserId, CommitmentNumber = CommitmentNumber});
            else
                server.CommitmentNumber += CommitmentNumber;
            await _context.SaveChangesAsync();
            
        }
        public async Task<int> GetNumberOfCommitments(ulong id)
        {
            var CommitmentNumber = await _context.Servers
                .Where(x => x.Id == id)
                .Select(x => x.CommitmentNumber)
                .FirstOrDefaultAsync();

            return await Task.FromResult(CommitmentNumber);
        }
        public async Task RemoveCommitment(ulong UserId, int CommitmentNumber)
        {

            var server = await _context.Servers
                .FindAsync(UserId);

            if (server == null)
                _context.Add(new Server { Id = UserId, UserId = UserId, CommitmentNumber = CommitmentNumber });
            else
                server.CommitmentNumber -= CommitmentNumber;
            await _context.SaveChangesAsync();

        }
        public async Task ModifyGuildPrefix(ulong id, string prefix)
        {
            var server = await _context.Servers
                .FindAsync(id);

            if (server == null)
                _context.Add(new Server { Id = id, Prefix = prefix });
            else
                server.Prefix = prefix;

            await _context.SaveChangesAsync();
        }

        public async Task<string> GetGuildPrefix(ulong id)
        {
            var prefix = await _context.Servers
                .Where(x => x.Id == id)
                .Select(x => x.Prefix)
                .FirstOrDefaultAsync();

            return await Task.FromResult(prefix);


        }
    }
}
