using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
	public static DataManager instance;

	public SaveData nowData = new SaveData();

	string path;
	string filename = "save";

	//데이터를 편하게 사용하기 위해 싱글톤으로 저장
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;

			path = Application.persistentDataPath + "/";
		}

		else if (instance != this)
		{
			Destroy(instance.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	//특정 데이터 저장하고 싶을시 DataManager.instance.nowData.name = object.text
	//세이브데이터 할시 DataManager.instance.SaveData();
	public void SaveData()
	{
		string data = JsonUtility.ToJson(nowData);

		File.WriteAllText(path + filename, data);
	}

	//데이터를 불러올때 쓴다. 현재 json에 저장되어있는 파일을 읽어 nowData에 넣는다
	public void LoadData()
	{
		string data = File.ReadAllText(path + filename);

		nowData = JsonUtility.FromJson<SaveData>(data);
	}

	//저장되어있는 데이터가 있는지 체크
	public bool DataCheck()
	{
		if (File.Exists(path + filename) == false)
		{
			Debug.Log("세이브 파일이 없어 새로 파일을 만듭니다");

			// Some Code...

			return false;
		}

		return true;
	}

}

[System.Serializable]
public class SaveData
{
	//순서 => 이름, 체력, 융통성, 사회성, 자신감, 지능,돈, 날짜, 시간, 분,스트레스지수
	public string name;
    public int hp;
    public int flex;
    public int social;
    public int conf;
	public int intel;
	public int money;
	public int day;
	public int hour;
	public int minute;
	public int stress;
}
