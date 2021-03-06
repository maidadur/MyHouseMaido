﻿namespace Maid.Core.DB
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	public interface IEntityRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAllAsync(SelectOptions options = null);

		IEnumerable<TEntity> GetAll(SelectOptions options = null);

		Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression, SelectOptions options = null);

		IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression);

		void Create(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		void Delete(Guid id);

		void Delete(Expression<Func<TEntity, bool>> expression);

		TEntity Get(Guid id);

		Task<TEntity> GetAsync(Guid id);

		Task SaveAsync();

		void Save();
	}
}