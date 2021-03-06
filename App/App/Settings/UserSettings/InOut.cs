﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InOut.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0) 
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the 
//   license terms of this work.  
// </license>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Settings.UserSettings
{
    using System;
    using System.Collections.Generic;

    using YANFOE.Models.IOModels;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;

    /// <summary>
    /// The in out.
    /// </summary>
    [Serializable]
    public class InOut
    {
        #region Constants and Fields

        /// <summary>
        /// The movie blu ray test path.
        /// </summary>
        private string movieBluRayTestPath = @"c:\DVD\The Dark Knight (2008)\bdmv\stream\00000.m2ts";

        /// <summary>
        /// The movie dvd test path.
        /// </summary>
        private string movieDVDTestPath = @"c:\DVD\The Dark Knight (2008)\VIDEO_TS\video_ts.ifo";

        /// <summary>
        /// The movie demo set name.
        /// </summary>
        private string movieDemoSetName = @"Batman Collection";

        /// <summary>
        /// The movie normal test path.
        /// </summary>
        private string movieNormalTestPath = @"c:\Movies\The Dark Knight (2008)\The Dark Knight(2008).avi";

        /// <summary>
        /// The rename tv.
        /// </summary>
        private bool renameTV;

        /// <summary>
        /// The tv bluray test path.
        /// </summary>
        private string tvBlurayTestPath =
            @"c:\TV\Arrested Development\Season 1\Arrested Development s01e01e02e03\bdmv\stream\00000.m2ts";

        /// <summary>
        /// The tv dvd test path.
        /// </summary>
        private string tvDVDTestPath =
            @"c:\TV\Arrested Development\Season 1\Arrested Development s01e01e02e03\VIDEO_TS\video_ts.ifo";

        /// <summary>
        /// The tv normal test path.
        /// </summary>
        private string tvNormalTestPath = @"c:\TV\Arrested Development\Season 1\Arrested Development s01e01 Title.avi";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InOut"/> class.
        /// </summary>
        public InOut()
        {
            this.TvEpisodeFileName = "<episodeFileName>";
            this.TvEpisodePath = "<episodePath>";
            this.TvFirstEpisodeOfSeason = "<firstEpisodeOfSeason>";
            this.TvFirstEpisodeOfSeasonPath = "<firstEpisodeOfSeasonPath>";
            this.TvFirstEpisodePathOfSeries = "<firstEpisodePath>";
            this.TvSeriesName = "<seriesName>";

            this.FillVideoExtentions();
            this.FillNfoExtentions();
            this.FillImageExtentions();
            this.FillMusicExtentions();

            this.FillNfoTypes();
            this.FillPosterType();
            this.FillFanartType();
            this.FillTrailerType();
            this.FillBannerType();
            this.FillEpisodeType();

            this.MovieSaveSettings = new Dictionary<NFOType, MovieSaveSettings>();
            this.CurrentMovieSaveSettings = new MovieSaveSettings();

            this.TvSaveSettings = new Dictionary<NFOType, TvSaveSettings>();
            this.CurrentTvSaveSettings = new TvSaveSettings();

            this.AddYAMJMoviesToDefault();
            this.AddYAMJTvToDefault();

            this.EpisodeNamingTemplate = "<seriesname> <season2><episode2> <episodename>";

            this.RenameTV = true;

            this.MinimumMovieSize = 104857600;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the minimum size of the movie.
        /// </summary>
        /// <value>
        /// The minimum size of the movie.
        /// </value>
        public int MinimumMovieSize { get; set; }

        /// <summary>
        /// Gets or sets BannerTypes.
        /// </summary>
        public List<string> BannerTypes { get; set; }

        /// <summary>
        /// Gets or sets CurrentMovieSaveSettings.
        /// </summary>
        public MovieSaveSettings CurrentMovieSaveSettings { get; set; }

        /// <summary>
        /// Gets or sets CurrentTvSaveSettings.
        /// </summary>
        public TvSaveSettings CurrentTvSaveSettings { get; set; }

        /// <summary>
        /// Gets or sets EpisodeNamingTemplate.
        /// </summary>
        public string EpisodeNamingTemplate { get; set; }

        /// <summary>
        /// Gets or sets EpisodeTypes.
        /// </summary>
        public List<string> EpisodeTypes { get; set; }

        /// <summary>
        /// Gets or sets FanartTypes.
        /// </summary>
        public List<string> FanartTypes { get; set; }

        /// <summary>
        /// Gets or sets TrailerTypes.
        /// </summary>
        public List<string> TrailerTypes { get; set; }

        /// <summary>
        /// Gets or sets ImageExtentions.
        /// </summary>
        public List<string> ImageExtentions { get; set; }

        /// <summary>
        /// Gets or sets IoType.
        /// </summary>
        public NFOType IoType { get; set; }

        /// <summary>
        /// Gets or sets MovieBlurayTestPath.
        /// </summary>
        public string MovieBlurayTestPath
        {
            get
            {
                return this.movieBluRayTestPath;
            }

            set
            {
                this.movieBluRayTestPath = value;
            }
        }

        /// <summary>
        /// Gets or sets MovieDVDTestPath.
        /// </summary>
        public string MovieDVDTestPath
        {
            get
            {
                return this.movieDVDTestPath;
            }

            set
            {
                this.movieDVDTestPath = value;
            }
        }

        /// <summary>
        /// Gets or sets MovieDemoSetName.
        /// </summary>
        public string MovieDemoSetName
        {
            get
            {
                return this.movieDemoSetName;
            }

            set
            {
                this.movieDemoSetName = value;
            }
        }

        /// <summary>
        /// Gets or sets MovieNormalTestPath.
        /// </summary>
        public string MovieNormalTestPath
        {
            get
            {
                return this.movieNormalTestPath;
            }

            set
            {
                this.movieNormalTestPath = value;
            }
        }

        /// <summary>
        /// Gets or sets MovieSaveSettings.
        /// </summary>
        public Dictionary<NFOType, MovieSaveSettings> MovieSaveSettings { get; set; }

        /// <summary>
        /// Gets or sets MusicExtentions.
        /// </summary>
        public List<string> MusicExtentions { get; set; }

        /// <summary>
        /// Gets or sets NFOTypes.
        /// </summary>
        public List<string> NFOTypes { get; set; }

        /// <summary>
        /// Gets or sets NfoExtentions.
        /// </summary>
        public List<string> NfoExtentions { get; set; }

        /// <summary>
        /// Gets or sets PosterTypes.
        /// </summary>
        public List<string> PosterTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether RenameTV.
        /// </summary>
        public bool RenameTV
        {
            get
            {
                return this.renameTV;
            }

            set
            {
                this.renameTV = value;
            }
        }

        /// <summary>
        /// Gets or sets TvBlurayTestPath.
        /// </summary>
        public string TvBlurayTestPath
        {
            get
            {
                return this.tvBlurayTestPath;
            }

            set
            {
                this.tvBlurayTestPath = value;
            }
        }

        /// <summary>
        /// Gets or sets TvDVDTestPath.
        /// </summary>
        public string TvDVDTestPath
        {
            get
            {
                return this.tvDVDTestPath;
            }

            set
            {
                this.tvDVDTestPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the tv episode file.
        /// </summary>
        /// <value>
        /// The name of the tv episode file.
        /// </value>
        public string TvEpisodeFileName { get; set; }

        /// <summary>
        /// Gets or sets the tv episode path.
        /// </summary>
        /// <value>
        /// The tv episode path.
        /// </value>
        public string TvEpisodePath { get; set; }

        /// <summary>
        /// Gets or sets the tv first episode of season.
        /// </summary>
        /// <value>
        /// The tv first episode of season.
        /// </value>
        public string TvFirstEpisodeOfSeason { get; set; }

        /// <summary>
        /// Gets or sets the tv first episode of season path.
        /// </summary>
        /// <value>
        /// The tv first episode of season path.
        /// </value>
        public string TvFirstEpisodeOfSeasonPath { get; set; }

        /// <summary>
        /// Gets or sets the tv first episode path of series.
        /// </summary>
        /// <value>
        /// The tv first episode path of series.
        /// </value>
        public string TvFirstEpisodePathOfSeries { get; set; }

        /// <summary>
        /// Gets or sets TvNormalTestPath.
        /// </summary>
        public string TvNormalTestPath
        {
            get
            {
                return this.tvNormalTestPath;
            }

            set
            {
                this.tvNormalTestPath = value;
            }
        }

        /// <summary>
        /// Gets or sets TvSaveSettings.
        /// </summary>
        public Dictionary<NFOType, TvSaveSettings> TvSaveSettings { get; set; }

        /// <summary>
        /// Gets or sets the name of the tv series.
        /// </summary>
        /// <value>
        /// The name of the tv series.
        /// </value>
        public string TvSeriesName { get; set; }

        /// <summary>
        /// Gets or sets VideoExtentions.
        /// </summary>
        public List<string> VideoExtentions { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The set current settings.
        /// </summary>
        /// <param name="type">
        /// The NFOType type.
        /// </param>
        public void SetCurrentSettings(NFOType type)
        {
            this.IoType = type;
            this.CurrentMovieSaveSettings = this.MovieSaveSettings[type].Clone();
            this.CurrentTvSaveSettings = this.TvSaveSettings[type].Clone();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the YAMJ Movie default settings to collection.
        /// </summary>
        private void AddYAMJMoviesToDefault()
        {
            this.MovieSaveSettings.Add(NFOType.YAMJ, new MovieSaveSettings());

            this.MovieSaveSettings[NFOType.YAMJ].NormalPosterNameTemplate = "<path><filename>.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].NormalFanartNameTemplate = "<path><filename>.fanart.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].NormalTrailerNameTemplate = "<path><filename>.[TRAILER-<trailername>].<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].NormalNfoNameTemplate = "<path><filename>.nfo";
            this.MovieSaveSettings[NFOType.YAMJ].NormalSetPosterNameTemplate = "<path>Set_<setname>_1.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].NormalSetFanartNameTemplate = "<path>Set_<setname>_1.fanart.<ext>";

            this.MovieSaveSettings[NFOType.YAMJ].DvdPosterNameTemplate = "<path><filename>.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].DvdFanartNameTemplate = "<path><filename>.fanart.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].DvdTrailerNameTemplate = "<path><filename>.[TRAILER-<trailername>].<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].DvdNfoNameTemplate = "<path><filename>.nfo";
            this.MovieSaveSettings[NFOType.YAMJ].DvdSetPosterNameTemplate = "<path>Set_<setname>_1.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].DvdSetFanartNameTemplate = "<path>Set_<setname>_1.fanart.<ext>";

            this.MovieSaveSettings[NFOType.YAMJ].BlurayPosterNameTemplate = "<path><filename>.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].BlurayFanartNameTemplate = "<path><filename>.fanart.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].BlurayTrailerNameTemplate = "<path><filename>.[TRAILER-<trailername>].<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].BlurayNfoNameTemplate = "<path><filename>.nfo";
            this.MovieSaveSettings[NFOType.YAMJ].BluraySetPosterNameTemplate = "<path>Set_<setname>_1.<ext>";
            this.MovieSaveSettings[NFOType.YAMJ].BluraySetFanartNameTemplate = "<path>Set_<setname>_1.fanart.<ext>";
        }

        /// <summary>
        /// Adds the YAMJ TV default settings to collection.
        /// </summary>
        private void AddYAMJTvToDefault()
        {
            this.TvSaveSettings.Add(NFOType.YAMJ, new TvSaveSettings());

            this.TvSaveSettings[NFOType.YAMJ].SeriesNfoTemplate = this.TvFirstEpisodePathOfSeries + "\\Set_" +
                                                                  this.TvSeriesName + "_1.nfo";
            this.TvSaveSettings[NFOType.YAMJ].SeriesPosterTemplate = string.Format(
                "{0}\\Set_{1}_1", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].SeriesBannerTemplate = string.Format(
                "{0}\\Set_{1}_1.banner", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].SeriesFanartTemplate = string.Format(
                "{0}\\Set_{1}_1.fanart", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);

            this.TvSaveSettings[NFOType.YAMJ].DVDSeriesNfoTemplate = string.Format(
                "{0}\\Set_{1}_1.nfo", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].DVDSeriesPosterTemplate = string.Format(
                "{0}\\Set_{1}_1", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].DVDSeriesBannerTemplate = string.Format(
                "{0}\\Set_{1}_1.banner", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].DVDSeriesFanartTemplate = string.Format(
                "{0}\\Set_{1}_1.fanart", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);

            this.TvSaveSettings[NFOType.YAMJ].BluraySeriesNfoTemplate = string.Format(
                "{0}\\Set_{1}_1.nfo", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].BluraySeriesPosterTemplate = string.Format(
                "{0}\\Set_{1}_1", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].BluraySeriesBannerTemplate = string.Format(
                "{0}\\Set_{1}_1.banner", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);
            this.TvSaveSettings[NFOType.YAMJ].BluraySeriesFanartTemplate = string.Format(
                "{0}\\Set_{1}_1.fanart", this.TvFirstEpisodePathOfSeries, this.TvSeriesName);

            this.TvSaveSettings[NFOType.YAMJ].SeasonPosterTemplate = string.Format(
                "{0}\\{1}", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);
            this.TvSaveSettings[NFOType.YAMJ].SeasonFanartTemplate = string.Format(
                "{0}\\{1}.fanart", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);
            this.TvSaveSettings[NFOType.YAMJ].SeasonBannerTemplate = string.Format(
                "{0}\\{1}.banner", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);

            this.TvSaveSettings[NFOType.YAMJ].DVDSeasonPosterTemplate = string.Format(
                "{0}\\{1}", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);
            this.TvSaveSettings[NFOType.YAMJ].DVDSeasonFanartTemplate = string.Format(
                "{0}\\{1}.fanart", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);
            this.TvSaveSettings[NFOType.YAMJ].DVDSeasonBannerTemplate = string.Format(
                "{0}\\{1}.banner", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);

            this.TvSaveSettings[NFOType.YAMJ].BluraySeasonPosterTemplate = string.Format(
                "{0}\\{1}", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);
            this.TvSaveSettings[NFOType.YAMJ].BluraySeasonFanartTemplate = string.Format(
                "{0}\\{1}.fanart", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);
            this.TvSaveSettings[NFOType.YAMJ].BluraySeasonBannerTemplate = string.Format(
                "{0}\\{1}.banner", this.TvFirstEpisodeOfSeasonPath, this.TvFirstEpisodeOfSeason);

            this.TvSaveSettings[NFOType.YAMJ].EpisodeNFOTemplate = this.TvEpisodePath + "\\" + this.TvEpisodeFileName +
                                                                   ".nfo";
            this.TvSaveSettings[NFOType.YAMJ].DVDEpisodeNFOTemplate = this.TvEpisodePath + "\\" + this.TvEpisodeFileName +
                                                                      ".nfo";
            this.TvSaveSettings[NFOType.YAMJ].BlurayEpisodeNFOTemplate = this.TvEpisodePath + "\\" +
                                                                         this.TvEpisodeFileName + ".nfo";

            this.TvSaveSettings[NFOType.YAMJ].EpisodeScreenshotTemplate = this.TvEpisodePath + "\\" +
                                                                          this.TvEpisodeFileName + ".videoimage";
            this.TvSaveSettings[NFOType.YAMJ].DVDEpisodeScreenshotTemplate = this.TvEpisodePath + "\\" +
                                                                             this.TvEpisodeFileName + ".videoimage";
            this.TvSaveSettings[NFOType.YAMJ].BlurayEpisodeScreenshotTemplate = this.TvEpisodePath + "\\" +
                                                                                this.TvEpisodeFileName + ".videoimage";

            this.TvSaveSettings[NFOType.YAMJ].RenameSeriesTemplate = "<seriesname>";
            this.TvSaveSettings[NFOType.YAMJ].RenameSeasonTemplate = "Season <seasonnumber>";
            this.TvSaveSettings[NFOType.YAMJ].RenameEpisodeTemplate =
                "<seriesname> s<season2>e<episode2> <episodename><ext>";
        }

        /// <summary>
        /// The fill banner type.
        /// </summary>
        private void FillBannerType()
        {
            this.BannerTypes = new List<string> { "<fileName>.banner" };
        }

        /// <summary>
        /// The fill episode type.
        /// </summary>
        private void FillEpisodeType()
        {
            this.EpisodeTypes = new List<string> { "<fileName>.videoimage" };
        }

        /// <summary>
        /// The fill fanart type.
        /// </summary>
        private void FillFanartType()
        {
            this.FanartTypes = new List<string> { "<fileName>-fanart", "<fileName>.fanart" };
        }

        /// <summary>
        /// The fill trailer type.
        /// </summary>
        private void FillTrailerType()
        {
            this.TrailerTypes = new List<string> { "<fileName>-[TRAILER-<trailerName>]", "<fileName>.[TRAILER-<trailerName>]" };
        }

        /// <summary>
        /// The fill image extentions.
        /// </summary>
        private void FillImageExtentions()
        {
            this.ImageExtentions = new List<string> { "jpg", "jpeg", "png", "gif", "tbn" };
        }

        /// <summary>
        /// The fill music extentions.
        /// </summary>
        private void FillMusicExtentions()
        {
            this.MusicExtentions = new List<string> { "mp3" };
        }

        /// <summary>
        /// The fill nfo extentions.
        /// </summary>
        private void FillNfoExtentions()
        {
            this.NfoExtentions = new List<string> { "nfo", "xml" };
        }

        /// <summary>
        /// The fill nfo types.
        /// </summary>
        private void FillNfoTypes()
        {
            this.NFOTypes = new List<string> { "<fileName>", "movie", "folder" };
        }

        /// <summary>
        /// The fill poster type.
        /// </summary>
        private void FillPosterType()
        {
            this.PosterTypes = new List<string> { "<fileName>" };
        }

        /// <summary>
        /// The fill video extentions.
        /// </summary>
        private void FillVideoExtentions()
        {
            this.VideoExtentions = new List<string>
                {
                    "avi",
                    "divx",
                    "mkv",
                    "wmv",
                    "m2ts",
                    "ts",
                    "rm",
                    "qt",
                    "iso",
                    "vob",
                    "mpg",
                    "mov",
                    "mp4",
                    "m1v",
                    "m2v",
                    "m4v",
                    "m2p",
                    "tp",
                    "trp",
                    "m2t",
                    "mts",
                    "asf",
                    "rmp4",
                    "img",
                    "ifo",
                    "bdmv"
                };
        }

        #endregion
    }
}