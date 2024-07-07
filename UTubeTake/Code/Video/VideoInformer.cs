using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace UTubeTake.Code {
    internal class VideoInformer {

        private VideoVariable _variable;

        public VideoInformer(VideoVariable variable) {
            _variable = variable;
        }

        public async Task LoadInfo(string url) { 
            _variable.video = await _variable.youtube.Videos.GetAsync(url);
        }

        public string[] GetInfoForVideo() {
            return new String[] { _variable.video.Title, _variable.video.Author.ToString(), _variable.video.Duration.ToString()};
        }

        public List<string> GetQualityList() {

            List<string> list = new List<string>();

            foreach(var key in _variable.videoQuality.Keys) {
                list.Add(key);
            }

            return list;
        }

        public List<string> GetBitRateList() {

            List<string> list = new List<string>();

            foreach(var key in _variable.videoBitRate.Keys) {
                list.Add(key);
            }

            return list;
        }

        public async Task LoadInfoForPicker(string url) {

            _variable.currentVideoStreams = await _variable.youtube.Videos.Streams.GetManifestAsync(url);

            //Video
            _variable.streamsVideo = _variable.currentVideoStreams.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).OrderByDescending(item => item.VideoQuality).ToList();

            _variable.videoQuality.Clear();
            _variable.videoQuality.Add("No video", -1);

            for(int i = 0, k = 0; i < _variable.streamsVideo.Count; i++, k++) {
                if (!_variable.videoQuality.ContainsKey(_variable.streamsVideo[i].VideoQuality.Label)) {
                    _variable.videoQuality.Add(_variable.streamsVideo[i].VideoQuality.Label, k);
                }
            }
            

            //Audio
            _variable.streamsAudio = _variable.currentVideoStreams.GetAudioOnlyStreams().Where(s => s.Container == Container.Mp4).OrderByDescending(item => item.Bitrate).ToList();

            _variable.videoBitRate.Clear();
            _variable.videoBitRate.Add("No sound", -1);

            for (int i = 0; i < _variable.streamsAudio.Count; i++) {
                if (!_variable.videoBitRate.ContainsKey(_variable.streamsAudio[i].Bitrate.ToString())) {
                    _variable.videoBitRate.Add(_variable.streamsAudio[i].Bitrate.ToString(), i);
                }
            }
        }

        public string GetSizeFile(int videoId, int soundId) {

            if (videoId <= 0 && soundId <= 0) return "0 Bytes";

            double size = 0;

            //Add memory
            if(videoId > 0) {
                IStreamInfo temp = _variable.streamsVideo[_variable.videoQuality.ElementAt(videoId).Value];
                size = size + temp.Size.Bytes;
            }

            if(soundId > 0) {
                IStreamInfo temp = _variable.streamsAudio[_variable.videoBitRate.ElementAt(soundId).Value];
                size = size + temp.Size.Bytes;
            }

            //Match memory
            string typeMemory = "Bytes";
            string[] arrayType = { "Kb", "Mb", "Gb", "B" };

            for (int i = 0; i < arrayType.Length; i++) {
                if (size / 1024.0 >= 1) {
                    size = size / 1024.0;
                    typeMemory = arrayType[i];
                }
            }

            return $"{size:0.##} {typeMemory}";
        }

    }
}
