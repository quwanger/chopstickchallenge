using UnityEngine;
using System.Collections;

public class CC_SoundManager : MonoBehaviour {
	
	public AudioClip[] accept;
	public AudioClip[] back;
	public AudioClip[] chopstickCollide;
	public AudioClip[] demotivational;
	public AudioClip[] dish;
	public AudioClip[] eating;
	public AudioClip[] fireworksExplosions;
	public AudioClip[] hover;
	public AudioClip[] intro;
	public AudioClip[] levelStart;
	public AudioClip[] lose;
	public AudioClip[] motivational;
	public AudioClip[] scoreIncrement;
	public AudioClip[] sushiCollide;
	public AudioClip[] sushiDrop;
	public AudioClip[] win;
	
	public void playSound(CC_Level.SoundType s){
		switch(s){
			case CC_Level.SoundType.accept:
				this.audio.PlayOneShot(accept[Random.Range(0, accept.Length)]);
				break;
			case CC_Level.SoundType.back:
				this.audio.PlayOneShot(back[Random.Range(0, back.Length)]);
				break;
			case CC_Level.SoundType.chopstickCollide:
				this.audio.PlayOneShot(chopstickCollide[Random.Range(0, chopstickCollide.Length)]);
				break;
			case CC_Level.SoundType.demotivational:
				this.audio.PlayOneShot(demotivational[Random.Range(0, demotivational.Length)]);
				break;
			case CC_Level.SoundType.dish:
				this.audio.PlayOneShot(dish[Random.Range(0, dish.Length)]);
				break;
			case CC_Level.SoundType.eating:
				this.audio.PlayOneShot(eating[Random.Range(0, eating.Length)]);
				break;
			case CC_Level.SoundType.fireworksExplosions:
				this.audio.PlayOneShot(fireworksExplosions[Random.Range(0, fireworksExplosions.Length)]);
				break;
			case CC_Level.SoundType.levelStart:
				this.audio.PlayOneShot(levelStart[Random.Range(0, levelStart.Length)]);
				break;
			case CC_Level.SoundType.scoreIncrement:
				this.audio.PlayOneShot(scoreIncrement[Random.Range(0, scoreIncrement.Length)]);
				break;
			case CC_Level.SoundType.sushiCollide:
				this.audio.PlayOneShot(sushiCollide[Random.Range(0, sushiCollide.Length)]);
				break;
			case CC_Level.SoundType.sushiDrop:
				this.audio.PlayOneShot(sushiDrop[Random.Range(0, sushiDrop.Length)]);
				break;
			case CC_Level.SoundType.win:
				this.audio.PlayOneShot(win[Random.Range(0, win.Length)]);
				break;
		}
	}
}
