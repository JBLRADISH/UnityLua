local Vector3 = {}

function Vector3.New(x, y, z)				
	local t = {x = x or 0, y = y or 0, z = z or 0}					
	return t
end

return Vector3