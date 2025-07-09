
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorVerifyEmailResendCode extends StatelessWidget {
  const DoctorVerifyEmailResendCode({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Text(
       S.of(context).resend_code,
        textAlign: TextAlign.center,
        style: AppTextStyles.popins500style18LightBlackColor.copyWith(
          fontSize: 14,
          color: AppColors.kPrimaryColor,
        ),
      ),
    );
  }
}
