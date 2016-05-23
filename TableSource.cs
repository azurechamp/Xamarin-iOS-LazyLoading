using System;

namespace Lazy
{
	public class TableSource
	{
		public TableSource ()
		{


			DownloadTask = Task.Factory.StartNew (() => { });
		}
	
		Task DownloadTask { get; set; }




		void BeginDownloadingImage (string ImageUrl, NSIndexPath indexPath, UITableView tableView)
		{
			byte[] data = null;

			UIImage returnImage = null;
			DownloadTask = DownloadTask.ContinueWith (prevTask => {
				try {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
					using (var c = new GzipWebClient ())
						data = c.DownloadData (ImageUrl);
				} finally {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				}
			});

			DownloadTask = DownloadTask.ContinueWith (t => {
				returnImage = UIImage.LoadFromData (NSData.FromArray (data));


				var cell =tableView.CellAt(indexPath);

				if (cell != null)
					cell.ImageView.Image = returnImage;

				//The Download task
			}, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext ());	
		}
	}
}

