﻿using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Student student;
    public Teacher teacher;
    public AudioSource teacherAudio;
    public AudioSource studentAudio;
    public AudioSource boardAudio;
    public AudioSource bellAudio;
    public AudioClip teacherTalk;
    public AudioClip teacherRage;
    public AudioClip teacherDone;
    public AudioClip studentSleeping;
    public AudioClip studentWaking;
    public UISystem ui;
    public bool _effect;
    private float _lowpassT;
    private float _lerpTime;
    private bool bellHasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        teacherAudio.loop = true;
        _effect = false;
        boardAudio.volume = 0.5f;
        teacherAudio.volume = 0.5f;
        bellAudio.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ui.gameStarted) {
            CheckWriting();
            DoEffect();

            if (!student._isSleeping && studentAudio == studentSleeping) studentAudio.Stop();

            bool teacherClipChanged = SetTeacherClip();
            bool studentClipChanged = CheckStudent();

            if (teacherClipChanged) teacherAudio.Play();
            if (studentClipChanged) studentAudio.Play();
            if (teacher.state == TeacherState.done && !bellAudio.isPlaying && !bellHasPlayed)
            {
                bellAudio.Play();
                bellHasPlayed = true;
            }
        }
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
        else if (teacher.state == TeacherState.raging)
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
                teacherAudio.Stop();
                teacherAudio.clip = teacherDone;
                teacherAudio.loop = false;
                changed = true;              
            }
        }
        return changed;
    }

    private bool CheckStudent()
    {
        bool changed = false;

        if (student._isSleeping && teacher.state == TeacherState.notWatching)
        {
            if (studentAudio.clip != studentSleeping)
            {
                studentAudio.clip = studentSleeping;
                studentAudio.loop = true;
                changed = true;
            }
        }
        else if (student._isSleeping && teacher.state == TeacherState.raging)
        {
            if (studentAudio.clip != studentWaking)
            {
                studentAudio.Stop();
                studentAudio.clip = studentWaking;
                studentAudio.loop = false;
                changed = true;
            }
        }
        else if (!student._isSleeping && teacher.state == TeacherState.notWatching)
        {
            if (studentAudio.clip == studentSleeping) studentAudio.Stop();
        }
        return changed;
    }

    private void CheckWriting()
    {
        if (teacher.state == TeacherState.notWatching)
        {
            if (!boardAudio.isPlaying) boardAudio.Play();
        }
        
        if (teacher.state == TeacherState.watching || teacher.state == TeacherState.raging) boardAudio.Stop();
    }

    private void DoEffect()
    {
        if (_effect && _lowpassT <= 1)
        {
            mixer.SetFloat("Lowpass", Mathf.Lerp(600, 22000, _lowpassT));
            _lowpassT = Time.deltaTime * _lerpTime;
        }

        if (!_effect && _lowpassT <= 1)
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

    public void ChangeVolume(float volume)
    {
        volume = Mathf.Clamp(volume, -80f, 0f);
        mixer.SetFloat("Master", volume);
    }
}
