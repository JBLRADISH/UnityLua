local Vector3 = require("Vector3")
local Quaternion = require("Quaternion")

function typeof(var)
    local _type = type(var)
    if _type == "table" or _type == "userdata" then
        local _meta = getmetatable(var)
        if (_meta ~= nil) then
        	return _meta["T"]
        end
    end
    return 0
end

local light = GameObject.Find("Directional Light")
GameObject.Instantiate(light, Vector3.New(10,10,10), Quaternion.New(0,0,0,0))