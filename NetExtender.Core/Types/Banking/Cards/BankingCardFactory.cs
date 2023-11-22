// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Banking.Cards.Interfaces;

namespace NetExtender.Types.Banking.Cards
{
    public abstract class BankingCardFactory : IBankingCardFactory
    {
        public abstract String Get();
    }
    
    public class BankingCardCustomerFactory : BankingCardFactory
    {
        protected String? Level { get; set; }
        protected String? CustomerId { get; set; }
        protected String Year { get; set; }

        public BankingCardCustomerFactory()
        {
            Year = DateTime.Now.ToString("yy");
        }

        public override String Get()
        {
            return Year + Level + CustomerId;
        }

        public BankingCardCustomerFactory WithLevel(Int32 level)
        {
            Level = level.ToString("D2");
            return this;
        }

        public BankingCardCustomerFactory WithCustomer(Int32 customer)
        {
            CustomerId = customer.ToString("D6");
            return this;
        }
    }
    
    public class BankingCardBankFactory : BankingCardFactory
    {
        protected String? PaymentType { get; set; }
        protected String? BankNumber { get; set; }
        protected String? BankInfo { get; set; }
        protected String? BankProgram { get; set; }
        protected String? ClientId { get; set; }

        public override String Get()
        {
            return PaymentType + BankNumber + BankInfo + BankProgram + ClientId;
        }

        public BankingCardBankFactory WithPaymentType(BankingCardType type)
        {
            return WithPaymentType((Int32) type);
        }

        public BankingCardBankFactory WithPaymentType(Int32 type)
        {
            return WithPaymentType(type.ToString());
        }

        public virtual BankingCardBankFactory WithPaymentType(String type)
        {
            PaymentType = type ?? throw new ArgumentNullException(nameof(type));
            return this;
        }

        public BankingCardBankFactory WithBank(Int32 id, Int32 info, Int32 program)
        {
            return WithBank(id.ToString(), info.ToString(), program.ToString());
        }

        public BankingCardBankFactory WithBank(Int32 id, String info, Int32 program)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return WithBank(id.ToString(), info, program.ToString());
        }

        public BankingCardBankFactory WithBank(Int32 id, Int32 info, String program)
        {
            if (program is null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            return WithBank(id.ToString(), info.ToString(), program);
        }

        public BankingCardBankFactory WithBank(String id, Int32 info, Int32 program)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return WithBank(id, info.ToString(), program.ToString());
        }

        public BankingCardBankFactory WithBank(String id, String info, Int32 program)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return WithBank(id, info, program.ToString());
        }

        public BankingCardBankFactory WithBank(String id, Int32 info, String program)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (program is null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            return WithBank(id, info.ToString(), program);
        }

        public BankingCardBankFactory WithBank(Int32 id, String info, String program)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (program is null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            return WithBank(id.ToString(), info, program);
        }

        public virtual BankingCardBankFactory WithBank(String id, String info, String program)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (program is null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            BankNumber = PadString(id, 3);
            BankInfo = PadString(info, 2);
            BankProgram = PadString(program, 2);
            return this;
        }

        public BankingCardBankFactory WithClient(Int32 client)
        {
            return WithClient(client.ToString());
        }

        public virtual BankingCardBankFactory WithClient(String client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            ClientId = PadString(client, 7);
            return this;
        }

        private static String PadString(String value, Int32 width)
        {
            return value.PadLeft(width, '0');
        }
    }
}