function hotfix(class, name, func)
	class[name.."HotFix"] = func
end

hotfix(HotFix, "Add", function(a,b)
    return (a + b) * 5
end)