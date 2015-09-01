using Microsoft.Xna.Framework;
using Yna.Engine;
using Yna.Engine.Audio;

namespace Imlost.Source.Sound
{
    public class AmbianceManager
    {

        private AudioManager _audioManager;

        private AmbianceZone _currentZone;
        private AmbianceZone _nextZone;

        private bool _switching;
        private bool _muteHeart;

        private float _heartbeat;
        private HB_pace _currentPace;

        private GuideSound _guideSound;

        private double _lastTick;
        private double _lastGuide;

        private bool _transitionDone;
        private double _transitionTimer;
        private TypeOfDeath _death;

        private string music_path_outside = "Audio/Outside";
        private string music_path_hall = "Audio/Hall";
        private string music_path_bathroom = "Audio/Bathroom";
        private string music_path_stairs = "Audio/stairs";
        private string music_path_endroom = "Audio/Endroom";

        private string sound_path_heartbeat_1 = "Audio/heartbeat_slow";
        private string sound_path_heartbeat_2 = "Audio/heartbeat_fast";
        private string sound_path_heartbeat_3 = "Audio/heartbeat_faster";

        private string sound_path_carhonk = "Audio/honk";
        private string sound_path_bell = "Audio/bell";
        private string sound_path_phone = "Audio/phone";

        private string sound_path_ghost = "Audio/ghost";
        private string sound_path_train_passes = "Audio/train_passes_short";
        public enum HB_pace
        {
            speed1,
            speed2,
            speed3
        }

        public enum AmbianceZone
        {
            Outside,
            Hall,
            Bathroom,
            Stairs,
            Room
        }

        public enum GuideSound
        {
            Carhonk,
            Phone,
            Bell,
            None
        }

        public enum TypeOfDeath
        {
            Train,
            Ghost,
            None
        }

        public AmbianceManager()
        {
            _heartbeat = 1.0f;
            _currentZone = AmbianceZone.Outside;
            _nextZone = _currentZone;
            _switching = false;
            _audioManager = YnG.AudioManager;
            _currentPace = HB_pace.speed1;
            _guideSound = GuideSound.None;
            _lastTick = 0;
            _lastGuide = 0;
            _death = TypeOfDeath.None;
            _transitionDone = false;
            _muteHeart = false;
            PlayAmbianceMusic();
            PlayHeartbeat();
        }

        private void PlayAmbianceMusic()
        {
            switch (_currentZone)
            {
                case AmbianceZone.Outside:
                    _audioManager.PlayMusic(music_path_outside, true);
                    break;
                case AmbianceZone.Hall:
                    _audioManager.PlayMusic(music_path_hall, true);
                    break;
                case AmbianceZone.Bathroom:
                    _audioManager.PlayMusic(music_path_bathroom, true);
                    break;
                case AmbianceZone.Stairs:
                    _audioManager.PlayMusic(music_path_stairs, true);
                    break;
                case AmbianceZone.Room:
                    _audioManager.PlayMusic(music_path_endroom, true);
                    break;
            }
        }

        public void SetGuideSound(GuideSound guide)
        {
            _guideSound = guide;
        }

        public bool TransitionDone()
        {
            return _transitionDone;
        }

        public void SetAmbianceZone(AmbianceZone newZone)
        {
            if (_currentZone.Equals(newZone))
            {
                //nothing happens
            }
            else
            {
                _nextZone = newZone;
                _switching = true;
            }
        }

        private bool MuteHeart
        {
            set
            {
                _muteHeart = value;
            }
        }

        public float Heartbeat
        {
            set
            {
                _heartbeat = value;
            }
        }

        public void BoostHeartbeat(float value)
        {
            _heartbeat -= value;
            if (_heartbeat < 0.4f)
            {
                _heartbeat = 0.4f;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!_death.Equals(TypeOfDeath.None))
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - _transitionTimer > 5 * 1000)
                {
                    _transitionDone = true;
                    _death = TypeOfDeath.None;
                }

                if (gameTime.TotalGameTime.TotalMilliseconds - _lastTick > _heartbeat * 1000)
                {
                    _lastTick = gameTime.TotalGameTime.TotalMilliseconds;
                    if (!_muteHeart) { PlayHeartbeat(); }
                }
            }
            else
            {
                _transitionTimer = gameTime.TotalGameTime.TotalMilliseconds;
                if (_guideSound.Equals(GuideSound.None))
                {
                    _lastGuide = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds - _lastGuide > 20 * 1000)
                {
                    _lastGuide = gameTime.TotalGameTime.TotalMilliseconds;
                    PlayGuide();
                }

                if (gameTime.TotalGameTime.TotalMilliseconds - _lastTick > _heartbeat * 1000)
                {
                    _lastTick = gameTime.TotalGameTime.TotalMilliseconds;
                    if (_heartbeat < 0.4f)
                    {
                        _heartbeat = 0.4f;
                    }
                    if (_heartbeat < 0.8f)
                    {
                        _heartbeat += 0.01f;
                    }
                    HB_pace newPace = GetPace();
                    if (_currentPace.Equals(newPace))
                    {
                        _currentPace = newPace;
                    }
                    if (!_muteHeart) { PlayHeartbeat(); }

                }
                if (_switching)
                {
                    if (!_nextZone.Equals(_currentZone))
                    {
                        if (_audioManager.MusicVolume > 0f)
                        {
                            _audioManager.MusicVolume -= 0.01f;
                        }
                        if (_audioManager.MusicVolume == 0f)
                        {
                            _currentZone = _nextZone;
                            PlayAmbianceMusic();
                        }
                    }
                    else
                    {
                        float max_volume = 0.5f;
                        if (_currentZone.Equals(AmbianceZone.Stairs) || _currentZone.Equals(AmbianceZone.Room)) { max_volume = 0.4f; }
                        if (_audioManager.MusicVolume < max_volume)
                        {
                            _audioManager.MusicVolume += 0.01f;
                        }
                        if (_audioManager.MusicVolume == max_volume)
                        {
                            _switching = false;
                        }
                    }
                }
            }
        }

        public void PlayDeath(TypeOfDeath death)
        {
            _transitionDone = false;
            _death = death;
            if (death.Equals(TypeOfDeath.Ghost))
            {
                _audioManager.PlaySound(sound_path_ghost, 0.6f, 0.0f, 0.0f);
            }
            if (death.Equals(TypeOfDeath.Train))
            {
                _audioManager.PlaySound(sound_path_train_passes, 0.6f, 0.0f, 0.0f);
            }
            BoostHeartbeat(0.4f);
        }

        private HB_pace GetPace()
        {
            if (_heartbeat < 0.8f)
            {
                if (_heartbeat < 0.7f)
                {
                    if (_heartbeat < 0.6f)
                    {
                        return HB_pace.speed3;
                    }
                    else
                    {
                        return HB_pace.speed2;
                    }
                }
                else
                {
                    return HB_pace.speed1;
                }
            }
            else
            {
                return HB_pace.speed1;
            }
        }

        private void PlayHeartbeat()
        {
            switch (_currentPace)
            {
                case HB_pace.speed1:
                    _audioManager.PlaySound(sound_path_heartbeat_1, 0.6f, 0.0f, 0.0f);
                    break;
                case HB_pace.speed2:
                    _audioManager.PlaySound(sound_path_heartbeat_2, 0.8f, 0.0f, 0.0f);
                    break;
                case HB_pace.speed3:
                    _audioManager.PlaySound(sound_path_heartbeat_3, 1.0f, 0.0f, 0.0f);
                    break;
            }
        }

        private void PlayGuide()
        {
            switch (_guideSound)
            {
                case GuideSound.Carhonk:
                    _audioManager.PlaySound(sound_path_carhonk, 0.6f, 0.0f, 0.0f);
                    BoostHeartbeat(0.2f);
                    break;
                case GuideSound.Phone:
                    _audioManager.PlaySound(sound_path_phone, 0.6f, 0.0f, 0.0f);
                    BoostHeartbeat(0.3f);
                    break;
                case GuideSound.Bell:
                    _audioManager.PlaySound(sound_path_bell, 0.6f, 0.0f, 0.0f);
                    BoostHeartbeat(0.2f);
                    break;
            }
        }
    }
}
