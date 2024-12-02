using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private GameObject basicFallingCursor;
    private Queue<GameObject> basicFallingCursorPool = new Queue<GameObject>();
    [SerializeField] private int basicFallingCursorPoolSize = 500;

    [SerializeField] private GameObject knifePrefab;
    private Queue<GameObject> knifePool = new Queue<GameObject>();
    [SerializeField] private int knifePoolSize = 25;

    [SerializeField] private GameObject shurikenPrefab;
    private Queue<GameObject> shurikenPool = new Queue<GameObject>();
    [SerializeField] private int shurikenPoolSize = 50;

    [SerializeField] private GameObject boulderPrefab;
    private Queue<GameObject> boulderPool = new Queue<GameObject>();
    [SerializeField] private int boulderPoolSize = 25;

    [SerializeField] private GameObject pointerPrefab;
    private Queue<GameObject> pointerPool = new Queue<GameObject>();
    [SerializeField] private int pointerPoolSize = 200;

    [SerializeField] private TextMeshProUGUI pointTextPrefab;
    private Queue<TextMeshProUGUI> pointTextPool = new Queue<TextMeshProUGUI>();
    [SerializeField] private int pointTextPoolSize = 200;

    [SerializeField] private TextMeshProUGUI fallingPointTextPrefab;
    private Queue<TextMeshProUGUI> fallingPointTextPool = new Queue<TextMeshProUGUI>();
    [SerializeField] private int fallingPointTextPoolSize = 200;

    [SerializeField] private GameObject laserPrefab;
    private Queue<GameObject> laserPool = new Queue<GameObject>();
    [SerializeField] private int laserPoolSize = 15;

    [SerializeField] private GameObject spearPrefab;
    private Queue<GameObject> spearPool = new Queue<GameObject>();
    [SerializeField] private int spearPoolSize = 40;

    [SerializeField] private GameObject spikePrefab;
    private Queue<GameObject> spikePool = new Queue<GameObject>();
    [SerializeField] private int spikePoolSize = 30;

    [SerializeField] private GameObject spikeCirclePrefab;
    private Queue<GameObject> spikeCirclePool = new Queue<GameObject>();
    [SerializeField] private int spikeCirclePoolSize = 20;

    [SerializeField] private GameObject bulletPrefab;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    [SerializeField] private int bulletPoolSize = 100;

    [SerializeField] private GameObject arrowPrefab;
    private Queue<GameObject> arrowPool = new Queue<GameObject>();
    [SerializeField] private int arrowPoolSize = 300;

    [SerializeField] private GameObject particlePrefab;
    private Queue<GameObject> particlePool = new Queue<GameObject>();
    [SerializeField] private int particlePoolSize = 300;

    [SerializeField] private GameObject boomerangPrefab;
    private Queue<GameObject> boomerangPool = new Queue<GameObject>();
    [SerializeField] private int boomerangPoolSize = 75;

    [SerializeField] private GameObject aoePrefab;
    private Queue<GameObject> aoePool = new Queue<GameObject>();
    [SerializeField] private int aoePoolsize = 300;

    //   ObjectPool.instance.ReturnGold5XFromPool(gameObject);
    // GameObject goldObject = ObjectPool.instance.GetGoldFromPool();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Transform fallingCursorParent, projectileParent, projectileParent2, textParent, pointerParent, particleParent;

    public void Start()
    {
        #region falling cursors
        for (int i = 0; i < basicFallingCursorPoolSize; i++)
        {
            GameObject cursor = Instantiate(basicFallingCursor);
            basicFallingCursorPool.Enqueue(cursor);
            cursor.SetActive(false);
            cursor.transform.SetParent(fallingCursorParent);
        }
        #endregion

        #region Knife
        for (int i = 0; i < knifePoolSize; i++)
        {
            GameObject knife = Instantiate(knifePrefab);
            knifePool.Enqueue(knife);
            knife.SetActive(false);
            knife.transform.SetParent(projectileParent);
            knife.transform.localScale = new Vector2(0.5f, 0.5f);
        }
        #endregion

        #region shuriken
        for (int i = 0; i < shurikenPoolSize; i++)
        {
            GameObject shuriken = Instantiate(shurikenPrefab);
            shurikenPool.Enqueue(shuriken);
            shuriken.SetActive(false);
            shuriken.transform.SetParent(projectileParent);
            shuriken.transform.localScale = new Vector2(0.15f, 0.15f);
        }
        #endregion

        #region Texts

        for (int i = 0; i < pointTextPoolSize; i++)
        {
            TextMeshProUGUI point = Instantiate(pointTextPrefab);
            pointTextPool.Enqueue(point);
            point.gameObject.SetActive(false);
            point.transform.SetParent(textParent);
        }

        for (int i = 0; i < fallingPointTextPoolSize; i++)
        {
            TextMeshProUGUI point = Instantiate(fallingPointTextPrefab);
            fallingPointTextPool.Enqueue(point);
            point.gameObject.SetActive(false);
            point.transform.SetParent(textParent);
        }
        #endregion

        #region boulder
        for (int i = 0; i < boulderPoolSize; i++)
        {
            GameObject boulder = Instantiate(boulderPrefab);
            boulderPool.Enqueue(boulder);
            boulder.gameObject.SetActive(false);
            boulder.transform.SetParent(projectileParent);
            boulder.transform.localScale = new Vector2(40, 40);
        }
        #endregion

        #region pointer
        for (int i = 0; i < pointerPoolSize; i++)
        {
            GameObject pointer = Instantiate(pointerPrefab);
            pointerPool.Enqueue(pointer);
            pointer.gameObject.SetActive(false);
            pointer.transform.SetParent(pointerParent);
            pointer.transform.localScale = new Vector2(90, 90);
        }
        #endregion

        #region laser
        for (int i = 0; i < laserPoolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
            laserPool.Enqueue(laser);
            laser.gameObject.SetActive(false);
            laser.transform.SetParent(projectileParent2);
            laser.transform.localScale = new Vector2(0.54f, 0.54f);
        }
        #endregion

        #region spear
        for (int i = 0; i < spearPoolSize; i++)
        {
            GameObject laser = Instantiate(spearPrefab);
            spearPool.Enqueue(laser);
            laser.gameObject.SetActive(false);
            laser.transform.SetParent(projectileParent);
            laser.transform.localScale = new Vector2(0.5f, 0.5f);
        }
        #endregion

        #region spike
        for (int i = 0; i < spikePoolSize; i++)
        {
            GameObject spike = Instantiate(spikePrefab);
            spikePool.Enqueue(spike);
            spike.gameObject.SetActive(false);
            spike.transform.SetParent(projectileParent2);
            spike.transform.localScale = new Vector2(1.5f, 1.5f);
        }
        #endregion

        #region spikecirle
        for (int i = 0; i < spikeCirclePoolSize; i++)
        {
            GameObject spikeCircle = Instantiate(spikeCirclePrefab);
            spikeCirclePool.Enqueue(spikeCircle);
            spikeCircle.gameObject.SetActive(false);
            spikeCircle.transform.SetParent(projectileParent);
            spikeCircle.transform.localScale = new Vector2(1.1f, 1.1f);
        }
        #endregion

        #region bullet
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bulletPool.Enqueue(bullet);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(projectileParent);
            bullet.transform.localScale = new Vector2(0.5f, 0.5f);
        }
        #endregion

        #region arrow
        for (int i = 0; i < arrowPoolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrowPool.Enqueue(arrow);
            arrow.gameObject.SetActive(false);
            arrow.transform.SetParent(projectileParent);
            arrow.transform.localScale = new Vector2(0.9f, 0.9f);
        }
        #endregion

        #region boomerang
        for (int i = 0; i < boomerangPoolSize; i++)
        {
            GameObject boomerang = Instantiate(boomerangPrefab);
            boomerangPool.Enqueue(boomerang);
            boomerang.gameObject.SetActive(false);
            boomerang.transform.SetParent(projectileParent);
            boomerang.transform.localScale = new Vector2(0.85f, 0.85f);
        }
        #endregion

        #region Particle falling cursors
        for (int i = 0; i < particlePoolSize; i++)
        {
            GameObject particle = Instantiate(particlePrefab);
            particlePool.Enqueue(particle);
            particle.gameObject.SetActive(false);
            particle.transform.SetParent(fallingCursorParent);
        }
        #endregion

        #region aeo
        for (int i = 0; i < aoePoolsize; i++)
        {
            GameObject aeo = Instantiate(aoePrefab);
            aoePool.Enqueue(aeo);
            aeo.gameObject.SetActive(false);
            aeo.transform.SetParent(pointerParent);
            aeo.transform.localPosition = new Vector3(1,1,1);
        }
        #endregion
    }

    #region Falling Cursors
    public GameObject GetFallingCursorFromPool()
    {
        if (basicFallingCursorPool.Count > 0)
        {
            GameObject cursor = basicFallingCursorPool.Dequeue();
            cursor.SetActive(true);
            return cursor;
        }
        else
        {
            GameObject cursor = Instantiate(basicFallingCursor);
            return cursor;

        }
    }

    public void ReturnFallingCursorFromPool(GameObject cursor)
    {
        basicFallingCursorPool.Enqueue(cursor);
        cursor.SetActive(false);
    }
    #endregion

    #region Knife
    public GameObject GetKnifeFromPool()
    {
        if (knifePool.Count > 0)
        {
            GameObject knife = knifePool.Dequeue();
            knife.SetActive(true);
            return knife;
        }
        else
        {
            GameObject knife = Instantiate(knifePrefab);
            return knife;
        }
    }

    public void ReturnKnifeFromPool(GameObject knife)
    {
        knifePool.Enqueue(knife);
        knife.SetActive(false);
    }
    #endregion

    #region Shuriken
    public GameObject GetShurikenFromPool()
    {
        if (shurikenPool.Count > 0)
        {
            GameObject shuriken = shurikenPool.Dequeue();
            shuriken.SetActive(true);
            return shuriken;
        }
        else
        {
            GameObject shuriken = Instantiate(shurikenPrefab);
            return shuriken;
        }
    }

    public void ReturnShurikenFromPool(GameObject shuriken)
    {
        shurikenPool.Enqueue(shuriken);
        shuriken.SetActive(false);
    }
    #endregion

    #region Boulder
    public GameObject GetBoulderFromPool()
    {
        if (boulderPool.Count > 0)
        {
            GameObject boulder = boulderPool.Dequeue();
            boulder.SetActive(true);
            return boulder;
        }
        else
        {
            GameObject boulder = Instantiate(boulderPrefab);
            return boulder;
        }
    }

    public void ReturnBoulderFromPool(GameObject boulder)
    {
        boulderPool.Enqueue(boulder);
        boulder.SetActive(false);
    }
    #endregion

    #region Pointer
    public GameObject GetPointerFromPool()
    {
        if (pointerPool.Count > 0)
        {
            GameObject pointer = pointerPool.Dequeue();
            pointer.SetActive(true);
            return pointer;
        }
        else
        {
            GameObject pointer = Instantiate(pointerPrefab);
            return pointer;
        }
    }

    public void ReturnPointerFromPool(GameObject pointer)
    {
        pointerPool.Enqueue(pointer);
        pointer.SetActive(false);
    }
    #endregion

    #region Text pop up from main cursor
    public TextMeshProUGUI GetTextPopUpFromPool()
    {
        if (pointTextPool.Count > 0)
        {
            TextMeshProUGUI text = pointTextPool.Dequeue();
            text.gameObject.SetActive(true);
            return text;
        }
        else
        {
            TextMeshProUGUI text = Instantiate(pointTextPrefab);
            return text;
        }
    }

    public void ReturnTextPopUpFromPool(TextMeshProUGUI text)
    {
        pointTextPool.Enqueue(text);
        text.gameObject.SetActive(false);
    }
    #endregion

    #region Text pop up from falling cursors
    public TextMeshProUGUI GetFallingTextPopUpFromPool()
    {
        if (fallingPointTextPool.Count > 0)
        {
            TextMeshProUGUI text = fallingPointTextPool.Dequeue();
            text.gameObject.SetActive(true);
            return text;
        }
        else
        {
            TextMeshProUGUI text = Instantiate(fallingPointTextPrefab);
            return text;
        }
    }

    public void ReturnFallingTextPopUpFromPool(TextMeshProUGUI text)
    {
        fallingPointTextPool.Enqueue(text);
        text.gameObject.SetActive(false);
    }
    #endregion

    #region Laser
    public GameObject GetLaserFromPool()
    {
        if (laserPool.Count > 0)
        {
            GameObject laser = laserPool.Dequeue();
            laser.SetActive(true);
            return laser;
        }
        else
        {
            GameObject laser = Instantiate(laserPrefab);
            return laser;
        }
    }

    public void ReturnLaserFromPool(GameObject laser)
    {
        laserPool.Enqueue(laser);
        laser.SetActive(false);
    }
    #endregion

    #region spear
    public GameObject GetSpearFromPool()
    {
        if (spearPool.Count > 0)
        {
            GameObject spear = spearPool.Dequeue();
            spear.SetActive(true);
            return spear;
        }
        else
        {
            GameObject spear = Instantiate(spearPrefab);
            return spear;
        }
    }

    public void ReturnSpearFromPool(GameObject spear)
    {
        spearPool.Enqueue(spear);
        spear.SetActive(false);
    }
    #endregion

    #region spike
    public GameObject GetSpikeFromPool()
    {
        if (spikePool.Count > 0)
        {
            GameObject spike = spikePool.Dequeue();
            spike.SetActive(true);
            return spike;
        }
        else
        {
            GameObject spike = Instantiate(spikePrefab);
            return spike;
        }
    }

    public void ReturnSpikeFromPool(GameObject spike)
    {
        spikePool.Enqueue(spike);
        spike.SetActive(false);
    }
    #endregion

    #region spikeCircle
    public GameObject GetSpikeCircleFromPool()
    {
        if (spikeCirclePool.Count > 0)
        {
            GameObject spikeCircle = spikeCirclePool.Dequeue();
            spikeCircle.SetActive(true);
            return spikeCircle;
        }
        else
        {
            GameObject spikeCircle = Instantiate(spikeCirclePrefab);
            return spikeCircle;
        }
    }

    public void ReturnSpikeCircleFromPool(GameObject spikeCircle)
    {
        spikeCirclePool.Enqueue(spikeCircle);
        spikeCircle.SetActive(false);
    }
    #endregion

    #region bullet
    public GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    public void ReturnBulletFromPool(GameObject bullet)
    {
        bulletPool.Enqueue(bullet);
        bullet.SetActive(false);
    }
    #endregion

    #region arrow
    public GameObject GetArrowFromPool()
    {
        if (arrowPool.Count > 0)
        {
            GameObject arrow = arrowPool.Dequeue();
            arrow.SetActive(true);
            return arrow;
        }
        else
        {
            GameObject arrow = Instantiate(arrowPrefab);
            return arrow;
        }
    }

    public void ReturnArrowFromPool(GameObject arrow)
    {
        arrowPool.Enqueue(arrow);
        arrow.SetActive(false);
    }
    #endregion

    #region boomerang
    public GameObject GetBoomerangFromPool()
    {
        if (boomerangPool.Count > 0)
        {
            GameObject boomerang = boomerangPool.Dequeue();
            boomerang.SetActive(true);
            return boomerang;
        }
        else
        {
            GameObject boomerang = Instantiate(boomerangPrefab);
            return boomerang;
        }
    }

    public void ReturnBoomerangFromPool(GameObject boomerang)
    {
        boomerangPool.Enqueue(boomerang);
        boomerang.SetActive(false);
    }
    #endregion

    #region Particle - falling cursors
    public GameObject GetParticleFromPool()
    {
        if (particlePool.Count > 0)
        {
            GameObject particle = particlePool.Dequeue();
            particle.SetActive(true);
            return particle;
        }
        else
        {
            GameObject particle = Instantiate(particlePrefab);
            return particle;
        }
    }

    public void ReturnParticleFromPool(GameObject particle)
    {
        particlePool.Enqueue(particle);
        particle.SetActive(false);
    }
    #endregion

    #region aoe
    public GameObject GetAOEfromPool()
    {
        if (aoePool.Count > 0)
        {
            GameObject aoe = aoePool.Dequeue();
            aoe.SetActive(true);
            return aoe;
        }
        else
        {
            GameObject aoe = Instantiate(aoePrefab);
            return aoe;
        }
    }

    public void ReturnAOEfromPool(GameObject aoe)
    {
        aoePool.Enqueue(aoe);
        aoe.SetActive(false);
    }
    #endregion
   
}
