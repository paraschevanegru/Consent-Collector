﻿using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ConsentContext context;
        public AnswerRepository(ConsentContext context)
        {
            this.context = context;
        }
        public async Task Create(Answer answer)
        {
            await this.context.Answer.AddAsync(answer);
        }

        public void Delete(Answer answer)
        {
            context.Answer.Remove(answer);
        }

        public async Task<Answer> GetAnswerById(Guid id)
        {
            return await context.Answer
                .FirstAsync(a => a.Id == id);
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(Answer answer)
        {
            this.context.Answer.Update(answer);
        }
    }
}