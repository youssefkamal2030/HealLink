
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';

class DoctorQrGenerateNewQrButton extends StatelessWidget {
  const DoctorQrGenerateNewQrButton({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: CustomElevatedButton(text: "Generate New Code", onPressed: () {} , 
      backGroundColor: AppColors.kWhiteColor,
      textColor: AppColors.kPrimaryColor
      ),
    );
  }
}
