using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public enum Clips
    {
        groan,
        groan2,
        groan3,
        groan4,
        groan5,
        groan6,
        lawnmower,
        losemusic,
        scream,
        ZombieHit1,
        ZombieHit2,
        ZombieHit3,
        Sun,
        plantBuy,
        plantShot1,
        plantShot2,
    }

    private AudioSource m_backGroundSource;
    //오디오믹서와 배경음악을 넣어줄것
    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private AudioClip m_BackGroundClip;

    private Transform pollingObjParentTrs;//풀링오브젝트의 부모위치

    //오디오클립이 들어간 비어있는 오디오소스
    [SerializeField] private GameObject SFXsource;
    //클립들을 넣어주고 이넘에 클립이랑 이름을똑같이 넣어주어야함
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    [SerializeField] private int poolingCount = 50;

    private bool bgSoundCheck = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        //StartCoroutine(bgStart());
        pollingObjParentTrs = transform.GetChild(0);
        initPoolingClip();
    }

    private void Start()
    {
        m_backGroundSource = GetComponent<AudioSource>();

    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            bgSoundPause();
            bgSoundCheck = false;
        }
        else
        {
            if (bgSoundCheck == false)
            {
                bgSoundCheck = true;
                StartCoroutine(bgStart());
            }
        }
    }
    private void initPoolingClip()
    {
        for (int i = 0; poolingCount > i; i++)
        {
            GameObject sfx = Instantiate(SFXsource);
            sfx.transform.SetParent(pollingObjParentTrs);
            sfx.SetActive(false);
        }
    }

    IEnumerator bgStart()
    {
        yield return new WaitForSeconds(0.5f);

        bgSoundPlay(m_BackGroundClip);
    }


    /// <summary>
    /// 내부 이넘을 통해서 사용할 클립을 선택, 볼륨 크기 , 사운드의 시작지점
    /// </summary>
    /// <param name="_clip">사용될 소리</param>
    /// <param name="_volum">소리의 크기</param>
    /// <param name="_SFXTime">소리의 시작지점</param>
    /// <param name="_parent">오브젝트의 부모</param>
    public void SFXCreate(Clips _clip, float _volum, float _SFXTime, Transform _parent)
    {
        AudioClip clip = clips.Find(x => x.name == _clip.ToString());
        StartCoroutine(SFXPlaying(clip, _volum, _SFXTime, _parent));
    }

    IEnumerator SFXPlaying(AudioClip clip, float _volum, float _SFXTime, Transform _parent)
    {
        GameObject SFXSource = getPoolingObject(_parent);

        AudioSource m_sfxaudiosource = SFXSource.GetComponent<AudioSource>();

        m_sfxaudiosource.outputAudioMixerGroup = m_mixer.FindMatchingGroups("SFX")[0];
        m_sfxaudiosource.clip = clip;
        m_sfxaudiosource.loop = false;
        m_sfxaudiosource.volume = _volum;
        m_sfxaudiosource.time = _SFXTime;
        m_sfxaudiosource.Play();
        yield return new WaitForSeconds(clip.length);
        removePooling(SFXSource);
    }

    private GameObject getPoolingObject(Transform _parent)
    {
        int parentCount = pollingObjParentTrs.childCount;
        GameObject obj = null;
        if (parentCount > 0)
        {
            obj = pollingObjParentTrs.GetChild(0).gameObject;

        }
        else
        {
            GameObject sfx = Instantiate(SFXsource);
            sfx.transform.SetParent(pollingObjParentTrs);
            sfx.SetActive(false);
            obj = sfx;
        }

        obj.transform.SetParent(_parent);
        obj.SetActive(true);

        return obj;
    }

    private void removePooling(GameObject _obj)
    {

        //자식의개수가 지정한것보다 부족할시 자식으로 다시넣어준다
        if (pollingObjParentTrs.childCount < poolingCount)
        {
            if (_obj == null)
            {
                _obj = Instantiate(SFXsource);
            }

            _obj.transform.SetParent(pollingObjParentTrs);
            _obj.SetActive(false);
            _obj.transform.position = Vector3.zero;

        }
        else
        {
            Destroy(_obj);
        }
    }


    private void bgSoundPlay(AudioClip clip)
    {

        m_backGroundSource.outputAudioMixerGroup = m_mixer.FindMatchingGroups("BGM")[0];
        m_backGroundSource.clip = clip;
        m_backGroundSource.loop = true;
        m_backGroundSource.time = 0;
        m_backGroundSource.volume = 1f;
        m_backGroundSource.Play();
    }

    public void bgSoundPause()
    {
        m_backGroundSource.Pause();
    }

    /// <summary>
    /// true 면 On false 일땐 Off
    /// </summary>
    /// <param name="_value"></param>
    public void SetSFXSound(bool _value)
    {
        if (_value)
        {
            m_mixer.SetFloat("SFX", 0);
        }
        else
        {
            m_mixer.SetFloat("SFX", -80);
        }
    }

    public void SetBGMSound(bool _value)
    {
        if (_value)
        {
            m_mixer.SetFloat("BGM", 0);
        }
        else
        {
            m_mixer.SetFloat("BGM", -80);
        }
    }
}
