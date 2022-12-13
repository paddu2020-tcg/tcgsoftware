using System;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for IncPagination.
	/// </summary>
	public class IncPagination
	{
		public IncPagination()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private PagedDataSource _paginatedData;
		private Int64 _numberOfPages;
		private Int64 _firstRowNumber;
		private Int64 _lastRowNumber;
		private Int64 _numberOfRowsPerPage;
		private int _activePageNumber;
		private Int64 _totalNumberOfRows;

		public Int64 NumberOfRowsPerPage
		{
			set
			{
				_numberOfRowsPerPage = value;
			}
			get
			{
				return _numberOfRowsPerPage;
			}
		}
		
		public PagedDataSource PaginatedData
		{
			set
			{
				_paginatedData = value;
			}
			get
			{
				return _paginatedData;
			}
		}

		public int ActivePageNumber
		{
			set
			{
				_activePageNumber = value;
			}
			get
			{
				return _activePageNumber;
			}
		}

		public Int64 NumberOfPages
		{
			get
			{
				return _numberOfPages;
			}
		}

		public Int64 FirstRowNumber
		{
			get
			{
				return _firstRowNumber;
			}
		}
		
		public Int64 LastRowNumber
		{
			get
			{
				return _lastRowNumber;
			}
		}

		public Int64 TotalNumberOfRows
		{
			get
			{
				return _totalNumberOfRows;
			}
		}

		public void Paginate()
		{
			_totalNumberOfRows = PaginatedData.DataSourceCount;

			if(0 < _totalNumberOfRows)
			{
				PaginatedData.PageSize = (int)_numberOfRowsPerPage;
				PaginatedData.CurrentPageIndex = ActivePageNumber;
				PaginatedData.AllowPaging = true;
			}

			_numberOfPages = Convert.ToInt64(System.Math.Ceiling((double)TotalNumberOfRows/(double)NumberOfRowsPerPage));
			
			if(0 == NumberOfPages)
			{
				_firstRowNumber = 0;
				_lastRowNumber = 0;
			}
			else
			{
				_firstRowNumber = ((ActivePageNumber - 1)*NumberOfRowsPerPage) + 1;

				if(ActivePageNumber == NumberOfPages)
				{
					_lastRowNumber = TotalNumberOfRows;
				}
				else
				{
					_lastRowNumber = ActivePageNumber*NumberOfRowsPerPage;
				}

			}
		}

	}
}
