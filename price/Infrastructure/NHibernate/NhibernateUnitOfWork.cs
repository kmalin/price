﻿using System;
using NHibernate;

namespace Infrastructure.NHibernate
{
    /// <summary>
    /// Implements Unit of work for NHibernate.
    /// </summary>
    public class NhibernateUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets current instance of the NhUnitOfWork.
        /// It gets the right instance that is related to current thread.
        /// </summary>
        public static NhibernateUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }
        [ThreadStatic]
        private static NhibernateUnitOfWork _current;

        /// <summary>
        /// Gets Nhibernate session object to perform queries.
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// Reference to the session factory.
        /// </summary>
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Reference to the currently running transcation.
        /// </summary>
        private ITransaction _transaction;

        /// <summary>
        /// Creates a new instance of NhUnitOfWork.
        /// </summary>
        /// <param name="sessionFactory"></param>
        public NhibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Opens database connection and begins transaction.
        /// </summary>
        public void BeginTransaction()
        {
            Session = _sessionFactory.OpenSession();
            //Session.FlushMode = FlushMode.Never;
            _transaction = Session.BeginTransaction();
        }

        /// <summary>
        /// Commits transaction and closes database connection.
        /// </summary>
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            finally
            {
                Session.Close();
            }
        }

        /// <summary>
        /// Rollbacks transaction and closes database connection.
        /// </summary>
        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                Session.Close();
            }
        }
    }
}
