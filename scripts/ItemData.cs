using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public enum Items
{
	none,
    healthPotion,
    bluePotion,
    gold
}


public static class ItemData
{
    static List<Resource> resources = new(){
        ResourceLoader.Load("res://assets/textures/items.png")
    };
    public static readonly Dictionary<Items, AtlasTexture> data = new(){
        {Items.none, CreateAtlas(-1, 0)},
        {Items.healthPotion, CreateAtlas(5, 0)},
        {Items.bluePotion, CreateAtlas(6, 0)},
        {Items.gold, CreateAtlas(3, 1)}
    };


    public static AtlasTexture CreateAtlas(int x, int y)
    {
        // if(resources.Single<Resource>(res => res.ResourcePath == "res://assets/textures/items.png") == null)
        //     resources.Add(ResourceLoader.Load("res://assets/textures/items.png"));
        AtlasTexture atlas = new AtlasTexture
        {
            Atlas = resources[0].Duplicate() as Texture2D,
            Region = new Rect2(x * 16, y * 16, 16, 16)
        };

        return atlas;
    }
    public static AtlasTexture CreateAtlas(int x, int y, string path)
    {
        AtlasTexture atlas = new AtlasTexture
        {
            Atlas = new CompressedTexture2D
            {
                // ResourcePath = path
            },
            Region = new Rect2(x * 16, y * 16, 16, 16)
        };

        return atlas;
    }
}