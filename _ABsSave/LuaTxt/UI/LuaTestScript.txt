local speed = 10
local lightCpnt = nil

function awake()
    print("lua awake...")
end
function start()
    print("lua start...")
    print("injected object", lightObject)
    --lightCpnt= lightObject:GetComponent(typeof(CS.UnityEngine.Light))
end

function update()
    local r = CS.UnityEngine.Vector3.up * CS.UnityEngine.Time.deltaTime * speed
    --self.transform:Rotate(r)
    --lightCpnt.color = CS.UnityEngine.Color(CS.UnityEngine.Mathf.Sin(CS.UnityEngine.Time.time) / 2 + 0.5, 0, 0, 1)
end

function ondestroy()
    print("lua destroy")
end