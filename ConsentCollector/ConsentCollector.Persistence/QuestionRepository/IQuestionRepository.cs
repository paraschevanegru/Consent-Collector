﻿using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.QuestionRepository
{
    public interface IQuestionRepository
    {
        Task<Question> GetQuestionById(Guid id);

        Task Create(Question question);

        Task SaveChanges();

        void Delete(Question question);
        void Update(Question question);
    }
}