﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerDate.src.Contracts
{
    public interface IConsumerDate
    {
        public Task ConsumerAsync(CancellationToken stopToken);
    }
}
