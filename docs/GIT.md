# Git & GitHub - Claims Processing System

Repository: [github.com/macfredd/ClaimsProcessingSystem](https://github.com/macfredd/ClaimsProcessingSystem)

---

## Use SSH (Recommended for Multiple GitHub Accounts)

When you have multiple GitHub accounts (e.g., work + personal), use **SSH** to avoid credential conflicts. SSH uses keys instead of cached HTTPS credentials.

### Verify Remote Uses SSH

```powershell
git remote -v
```

Should show:
```
origin  git@github.com:macfredd/ClaimsProcessingSystem.git (fetch)
origin  git@github.com:macfredd/ClaimsProcessingSystem.git (push)
```

### Switch to SSH (if using HTTPS)

```powershell
git remote set-url origin git@github.com:macfredd/ClaimsProcessingSystem.git
```

---

## Common Commands

| Action | Command |
|--------|---------|
| Status | `git status` |
| Add all | `git add .` |
| Commit | `git commit -m "Your message"` |
| Push | `git push origin main` |
| Pull | `git pull origin main` |
| Create branch | `git checkout -b feature/name` |

---

## Clone (New Machine)

**SSH (recommended):**
```powershell
git clone git@github.com:macfredd/ClaimsProcessingSystem.git
cd ClaimsProcessingSystem
```

**HTTPS:**
```powershell
git clone https://github.com/macfredd/ClaimsProcessingSystem.git
cd ClaimsProcessingSystem
```

---

## SSH Setup (Multiple Accounts)

If you need to set up SSH for the macfredd account on a new machine, see [ENVIRONMENT_SETUP.md](ENVIRONMENT_SETUP.md) or the SSH configuration steps in the project history.

---

## Fix: "The authenticity of host 'github.com' can't be established"

If SSH asks for confirmation every time you connect, add GitHub's host key to `known_hosts`:

```powershell
$githubKey = "github.com ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIOMqqnkVzrm0SdG6UOoqKLsabgH5C9okWi0dh2l9GKJl"
Add-Content -Path $HOME\.ssh\known_hosts -Value $githubKey
```

Verify:
```powershell
ssh -T git@github.com
```

Should respond: `Hi macfredd! You've successfully authenticated...` without prompting.
