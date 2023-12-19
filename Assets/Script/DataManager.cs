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

	//데이터가 중첩되서 저장되는것을 막기위해 싱글톤으로 저장
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

	public void LoadData()
	{
		string data = File.ReadAllText(path + filename);

		nowData = JsonUtility.FromJson<SaveData>(data);
	}

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
	//순서 => 이름, 체력, 융통성, 사회성, 자신감, 돈, 날짜
	public string name;
    public int hp;
    public int flex;
    public int social;
    public int conf;
    public string money;
	public int Day;
}
