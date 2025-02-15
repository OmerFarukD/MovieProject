using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;
using MovieProject.Model.Dtos.Users;
using MovieProject.Service.Abstracts;
using MovieProject.Service.BusinessRules.Users;
using MovieProject.Service.Constants.Users;

namespace MovieProject.Service.Concretes;
public sealed class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly UserBusinessRules _userBusinessRules;
    private readonly IMapper _mapper;

    public AuthService(IUserService userService, UserBusinessRules userBusinessRules, IMapper mapper)
    {
        _userService = userService;
        _userBusinessRules = userBusinessRules;
        _mapper = mapper;
    }

    public async Task<string> LoginAsync(LoginRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        // todo: Email kontrolü yap
        await _userBusinessRules.SearchByEmailAsync(requestDto.Email);

        var user = await _userService.GetByEmailAsync(requestDto.Email);
        var verifyPassword = HashingHelper
            .VerifyPasswordHash(requestDto.Password,user.PasswordHash,user.PasswordSalt);

        if (!verifyPassword)
            throw new BusinessException(UserMessages.PasswordIsWrong);

        return "Giriş Başarılı.";


        // Parola eşleşiyor mu ? 

    }

    public async Task<string> RegisterAsync(RegisterRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        User user = _mapper.Map<User>(requestDto);

        var hashResult = HashingHelper.CreatePasswordHash(requestDto.Password);

        user.PasswordHash = hashResult.passwordHash;
        user.PasswordSalt = hashResult.passwordSalt;

        await _userService.AddAsync(user);

        return "Kayıt başarılı şekilde oluştu.";

    }
}
