using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Student student;
    public Teacher teacher;
    public AudioSource teacherAudio;
    public AudioSource studentAudio;
    public AudioSource boardAudio;
    public AudioClip teacherTalk;
    public AudioClip teacherRage;
    public AudioClip teacherDone;
    public AudioClip sleeping;
    public AudioClip waking;
    public AudioClip writing;
    public bool _effect;
    private float _lowpassT;
    private float _lerpTime;

    // Start is called before the first frame update
    void Start()
    {
        boardAudio.clip = writing;
        _effect = false;
        teacherAudio.volume = 0.75f;    // FOR PROTO AUDIOS ONLY
    }

    // Update is called once per frame
    void Update()
    {
        bool teacherClipChanged = SetTeacherClip();
        
        if (teacherClipChanged) teacherAudio.Play();



        DoEffect();
    }

    private bool SetTeacherClip()
    {
        bool changed = false;
        
        if (teacher.state == TeacherState.watching || teacher.state == TeacherState.notWatching)
        {
            if (teacherAudio.clip != teacherTalk)
            {
                teacherAudio.clip = teacherTalk;
                changed = true;
            }
        }
        
        if (teacher.state == TeacherState.raging)
        {
            if (teacherAudio.clip != teacherRage)
            {
                teacherAudio.clip = teacherRage; 
                changed = true;
            }
        }

        if (teacher.state == TeacherState.done)
        {
            if (teacherAudio.clip != teacherDone)
            {
                teacherAudio.clip = teacherDone;
                changed = true;
            }
        }
        return changed;
    }

    private void CheckWriting()
    {
        if (teacher.state == TeacherState.notWatching)
        {
            if (!boardAudio.isPlaying) boardAudio.Play();
        }
        else boardAudio.Stop();
    }

    private void DoEffect()
    {
        if (_effect && _lerpTime <= 1)
        {
            mixer.SetFloat("Lowpass", Mathf.Lerp(600, 22000, _lowpassT));
            _lowpassT = Time.deltaTime * _lerpTime;
        }

        if (!_effect && _lerpTime <= 1)
        {
            mixer.SetFloat("Lowpass", Mathf.Lerp(22000, 600, _lowpassT));
            _lowpassT = Time.deltaTime * _lerpTime;
        }
    }

    public void Effect(bool effect, float time)
    {
        _effect = effect;
        _lowpassT = 0;
        _lerpTime = time;
    }
}
