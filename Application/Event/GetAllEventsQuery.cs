using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Events
{
   
    public record GetAllEventsQuery(string? Category = null) : IRequest<List<Event>>;

    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<Event>>
    {
        private readonly Context _context;
        public GetAllEventsHandler(Context context) => _context = context;

        public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(e => e.Category == request.Category);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}