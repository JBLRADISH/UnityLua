local Vector3 = {}

function Vector3.New(x, y, z)				
	local v = {x = x or 0, y = y or 0, z = z or 0}					
	return v
end

return Vector3